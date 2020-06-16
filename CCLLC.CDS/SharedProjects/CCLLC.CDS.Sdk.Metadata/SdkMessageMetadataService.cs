using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk.Metadata
{
    using CCLLC.CDS.Sdk;
    using Proxy;

    public class SdkMessageMetadataService : ISdkMessageMetadataService
    {

        private readonly eEndpoint Endpoint;

        public SdkMessageMetadataService(eEndpoint endpoint = eEndpoint.OrgService)
        {
            Endpoint = endpoint;
        }

        public IEnumerable<string> GetSdkMessageNames(IOrganizationService orgService, bool includeNonVisibleMessages = false)
        {
            if (includeNonVisibleMessages)
            {
                return new ExecutableFluentQuery<SdkMessage>(orgService)
                    .Select(cols => new { cols.Name })                    
                    .RetrieveAll()
                        .Select(r => r.Name);
            }

            return new ExecutableFluentQuery<SdkMessage>(orgService)
                    .Select(cols => new { cols.Name })
                    .InnerJoin<SdkMessageFilter>(SdkMessage.Fields.SdkMessageId, SdkMessageFilter.Fields.SdkMessageId, f => f
                        .WhereAll(e => e.Attribute("isvisible").Equals(true)))
                    .With.UniqueRecords()
                    .RetrieveAll()                        
                        .Select(r => r.Name);
        }

        public IEnumerable<SdkMessageMetadata> GetSdkMessageMetadata(IOrganizationService orgService, IEnumerable<string> messageNames)
        {
            var sdkMessages = new List<SdkMessageMetadata>();

            foreach (var messageName in messageNames)
            {
                var records = GetSdkMessageRecordsForMessageName(orgService, messageName);

                sdkMessages.AddRange(ParseSdkMessageMetadataFromRecords(records));
            }

            return sdkMessages;

        }

        private IEnumerable<Entity> GetSdkMessageRecordsForMessageName(IOrganizationService orgService, string messageName)
        {
            string endpointName = this.Endpoint == eEndpoint.OrgService ? "2011/Organization.svc" : "api/data";

            var records = new ExecutableFluentQuery<SdkMessage>(orgService)
                    .Select(cols => new { cols.SdkMessageId, cols.Name, cols.IsPrivate, cols.CustomizationLevel })
                    .WhereAll(e => e.Attribute(SdkMessage.Fields.Name).IsEqualTo(messageName))
                    .InnerJoin<SdkMessageFilter>(SdkMessage.Fields.SdkMessageId, SdkMessageFilter.Fields.SdkMessageId, f => f
                        .With.Alias("filter")
                        .Select(cols => new { cols.SdkMessageFilterId, cols.PrimaryObjectTypeCode, cols.SecondaryObjectTypeCode, cols.IsVisible })
                        .WhereAll(e => e.Attribute(SdkMessageFilter.Fields.IsVisible).IsEqualTo(true))
                    .InnerJoin<SdkMessagePair>(SdkMessage.Fields.SdkMessageId, SdkMessagePair.Fields.SdkMessageId, p => p
                        .With.Alias("pair")
                        .Select(cols => new { cols.SdkMessagePairId, cols.Namespace, cols.Endpoint })
                        .WhereAll(e => e.Attribute(SdkMessagePair.Fields.Endpoint).IsEqualTo(endpointName))
                        .LeftJoin<SdkMessageRequest>(SdkMessagePair.Fields.SdkMessagePairId, SdkMessageRequest.Fields.SdkMessagePairId, req => req
                            .With.Alias("request")
                            .Select(cols => new { cols.SdkMessageRequestId, cols.Name })
                            .LeftJoin<SdkMessageRequestField>(SdkMessageRequest.Fields.SdkMessageRequestId, SdkMessageRequestField.Fields.SdkMessageRequestId, fields => fields
                                .With.Alias("requestfield")
                                .Select(cols => new { cols.Name, cols.Optional, cols.Position, cols.PublicName, cols.ClrParser })
                                .OrderByAsc(SdkMessageRequestField.Fields.SdkMessageRequestFieldId))
                            .LeftJoin<SdkMessageResponse>(SdkMessageRequest.Fields.SdkMessageRequestId, SdkMessageResponse.Fields.SdkMessageRequestId, resp => resp
                                .With.Alias("response")
                                .Select(cols => new { cols.SdkMessageResponseId })
                                .LeftJoin<SdkMessageResponseField>(SdkMessageResponse.Fields.SdkMessageResponseId, SdkMessageResponseField.Fields.SdkMessageResponseId, fields => fields
                                    .With.Alias("responsefield")
                                    .Select(cols => new { cols.Name, cols.Value, cols.Position, cols.PublicName, cols.ClrFormatter })
                                    .OrderByAsc(SdkMessageResponseField.Fields.SdkMessageResponseFieldId))))))
                    .RetrieveAll();

            return records;
        }
     
        private IEnumerable<SdkMessageMetadata> ParseSdkMessageMetadataFromRecords(IEnumerable<Entity> records)
        {
            return records
                .GroupBy(e => e.GetAttributeValue<Guid>("sdkmessageid"))
                .Select(m => new SdkMessageMetadata(
                    id: m.FirstOrDefault().Id,
                    name: m.FirstOrDefault().GetAttributeValue<string>("name"),
                    isPrivate: m.FirstOrDefault().GetAttributeValue<bool?>("isprivate") == true,
                    isCustomAction: m.FirstOrDefault().GetAttributeValue<bool?>("ismanaged") != true,
                    filters: ParseMessageFilterMetadataFromRecordGroup(m),
                    messagePairs: ParseMessagePairMetadataFromRecordGroup(m)))
                .ToArray();       
        }
    
        private IEnumerable<SdkMessageFilterMetadata> ParseMessageFilterMetadataFromRecordGroup(IGrouping<Guid,Entity> recordGroup)
        {
            return recordGroup
                .Where(e => e.Contains("filter.sdkmessagefilterid") 
                    && e["filter.sdkmessagefilterid"] != null)
                .GroupBy(e => e.GetAliasedValue<Guid>("filter.sdkmessagefilterid"))
                .Select(e => new SdkMessageFilterMetadata(
                    id: e.FirstOrDefault().GetAliasedValue<Guid>("filter.sdkmessagefilterid"),
                    primaryObjectTypeCode: e.FirstOrDefault().GetAliasedValue<string>("filter.primaryobjecttypecode"),
                    secondaryObjectTypeCode: e.FirstOrDefault().GetAliasedValue<string>("filter.secondaryobjecttypecode"),
                    isVisible: e.FirstOrDefault().GetAliasedValue<bool?>("filter.isvisible") == true))
                .ToArray();
        }

        private IEnumerable<SdkMessagePairMetadata> ParseMessagePairMetadataFromRecordGroup(IGrouping<Guid, Entity> recordGroup)
        {
            return recordGroup
                .Where(e => e.Contains("pair.sdkmessagepairid") &&
                    e["pair.sdkmessagepairid"] != null)
                .GroupBy(e => e.GetAliasedValue<Guid>("pair.sdkmessagepairid"))
                .Select(e => new SdkMessagePairMetadata(
                    id: e.FirstOrDefault().GetAliasedValue<Guid>("pair.sdkmessagepairid"),
                    messageNamespace: e.FirstOrDefault().GetAliasedValue<string>("pair.namespace"),
                    requestMetadata: ParseRequestMetadataFromRecordGroup(e),
                    responseMetadata: ParseResponseMetadataFromRecordGroup(e)))
                .ToArray();
        }

        private SdkMessageRequestMetadata ParseRequestMetadataFromRecordGroup(IGrouping<Guid,Entity> recordGroup) 
        {
            return recordGroup
                .Where(req => req.Attributes.ContainsKey("request.sdkmessagerequestid")
                   && req.Attributes["request.sdkmessagerequestid"] != null)
                .GroupBy(req => req.GetAliasedValue<Guid>("request.sdkmessagerequestid"))
                .Select(req => new SdkMessageRequestMetadata(
                    id: req.FirstOrDefault().GetAliasedValue<Guid>("request.sdkmessagerequestid"),
                    name: req.FirstOrDefault().GetAliasedValue<string>("request.name"),
                    fields: ParseRequestFieldMetadataFromRecordGroup(req)
                )).FirstOrDefault();
        }

        private IEnumerable<SdkMessageRequestFieldMetadata> ParseRequestFieldMetadataFromRecordGroup(IGrouping<Guid,Entity> recordGroup)
        {
            return recordGroup
                .Where(e => e.Contains("requestfield.name")
                    && e["requestfield.name"] != null)
                .GroupBy(field => field.GetAliasedValue<string>("requestfield.name"))
                    .Select(field => new SdkMessageRequestFieldMetadata(
                        index: field.FirstOrDefault().GetAliasedValue<int>("requestfield.position"),
                        name: field.FirstOrDefault().GetAliasedValue<string>("requestfield.name"),
                        clrFormatter: field.FirstOrDefault().GetAliasedValue<string>("requestfield.clrparser"),
                        isOptional: field.FirstOrDefault().GetAliasedValue<bool?>("requestfield.isoptional").GetValueOrDefault()))
                    .ToArray();
        }

        private SdkMessageResponseMetadata ParseResponseMetadataFromRecordGroup(IGrouping<Guid, Entity> recordGroup)
        {
            return recordGroup
                .Where(resp => resp.Attributes.ContainsKey("response.sdkmessageresponseid")
                    && resp.Attributes["response.sdkmessageresponseid"] != null)
                .GroupBy(resp => resp.GetAliasedValue<Guid>("response.sdkmessageresponseid"))
                .Select(resp => new SdkMessageResponseMetadata(
                    id: resp.FirstOrDefault().GetAliasedValue<Guid>("response.sdkmessageresponseid"),
                    fields: ParseResponseFieldMetadataFromRecordGroup(resp)
                )).FirstOrDefault();
        }

        private IEnumerable<SdkMessageResponseFieldMetadata> ParseResponseFieldMetadataFromRecordGroup(IGrouping<Guid, Entity> recordGroup)
        {
            return recordGroup
                .Where(e => e.Contains("responsefield.name")
                    && e["responsefield.name"] != null)
                .GroupBy(field => field.GetAliasedValue<string>("responsefield.name"))
                    .Select(field => new SdkMessageResponseFieldMetadata(
                        index: field.FirstOrDefault().GetAliasedValue<int>("responsefield.position"),
                        name: field.FirstOrDefault().GetAliasedValue<string>("responsefield.name"),
                        value: field.FirstOrDefault().GetAliasedValue<string>("responsefield.value"),
                        clrFormatter: field.FirstOrDefault().GetAliasedValue<string>("responsefield.clrformatter")
                    )).ToArray();
        }
    }
}
