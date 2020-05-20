using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk.Metadata
{
    using CCLLC.CDS.Sdk.MessageMetadata;
    using CCLLC.CDS.Sdk;
   
    public class SdkMessageMetadataService
    {       

        public IEnumerable<string> GetSdkMessageNames(IOrganizationService orgService)
        {
            return new ExecutableFluentQuery<Proxy.SdkMessage>(orgService)
                    .Select(cols => new { cols.Name })
                    .InnerJoin<Proxy.SdkMessageFilter>(Proxy.SdkMessage.Fields.SdkMessageId, Proxy.SdkMessageFilter.Fields.SdkMessageId, f => f
                        .With.Alias("filter")
                        .Select(cols => new { cols.SdkMessageFilterId, cols.PrimaryObjectTypeCode, cols.SecondaryObjectTypeCode, cols.IsVisible })
                        .WhereAll(e => e.Attribute(Proxy.SdkMessageFilter.Fields.IsVisible).IsEqualTo(true)))                   
                    .RetrieveAll().Select(r => r.Name);
        }


        public IEnumerable<SdkMessage> GetSdkMessages(IOrganizationService orgService, IEnumerable<string> messageNames, eEndpoint endpoint = eEndpoint.OrgService)
        {
            var sdkMessages = new List<SdkMessage>();

            foreach (var messageName in messageNames)
            {
                var records = GetSdkMessageRecordsForMessageName(orgService, messageName, endpoint);

                sdkMessages.AddRange(ParseRecordsToSdkMessages(records));
            }

            return sdkMessages;

        }

        private IEnumerable<Entity> GetSdkMessageRecordsForMessageName(IOrganizationService orgService, string messageName, eEndpoint endpoint = eEndpoint.OrgService)
        {
            string endpointName = endpoint == eEndpoint.OrgService ? "2011/Organization.svc" : "api/data";

            var records = new CCLLC.CDS.Sdk.ExecutableFluentQuery<Proxy.SdkMessage>(orgService)
                    .Select(cols => new { cols.SdkMessageId, cols.Name, cols.IsPrivate, cols.CustomizationLevel })
                    .WhereAll(e => e.Attribute(Proxy.SdkMessage.Fields.Name).IsEqualTo(messageName))
                    .InnerJoin<Proxy.SdkMessageFilter>(Proxy.SdkMessage.Fields.SdkMessageId, Proxy.SdkMessageFilter.Fields.SdkMessageId, f => f
                        .With.Alias("filter")
                        .Select(cols => new { cols.SdkMessageFilterId, cols.PrimaryObjectTypeCode, cols.SecondaryObjectTypeCode, cols.IsVisible })
                        .WhereAll(e => e.Attribute(Proxy.SdkMessageFilter.Fields.IsVisible).IsEqualTo(true))
                    .InnerJoin<Proxy.SdkMessagePair>(Proxy.SdkMessage.Fields.SdkMessageId, Proxy.SdkMessagePair.Fields.SdkMessageId, p => p
                        .With.Alias("pair")
                        .Select(cols => new { cols.SdkMessagePairId, cols.Namespace, cols.Endpoint })
                        .WhereAll(e => e.Attribute(Proxy.SdkMessagePair.Fields.Endpoint).IsEqualTo(endpointName))
                        .LeftJoin<Proxy.SdkMessageRequest>(Proxy.SdkMessagePair.Fields.SdkMessagePairId, Proxy.SdkMessageRequest.Fields.SdkMessagePairId, req => req
                            .With.Alias("request")
                            .Select(cols => new { cols.SdkMessageRequestId, cols.Name })
                            .LeftJoin<Proxy.SdkMessageRequestField>(Proxy.SdkMessageRequest.Fields.SdkMessageRequestId, Proxy.SdkMessageRequestField.Fields.SdkMessageRequestId, fields => fields
                                .With.Alias("requestfield")
                                .Select(cols => new { cols.Name, cols.Optional, cols.Position, cols.PublicName, cols.ClrParser })
                                .OrderByAsc(Proxy.SdkMessageRequestField.Fields.SdkMessageRequestFieldId))
                            .LeftJoin<Proxy.SdkMessageResponse>(Proxy.SdkMessageRequest.Fields.SdkMessageRequestId, Proxy.SdkMessageResponse.Fields.SdkMessageRequestId, resp => resp
                                .With.Alias("response")
                                .Select(cols => new { cols.SdkMessageResponseId })
                                .LeftJoin<Proxy.SdkMessageResponseField>(Proxy.SdkMessageResponse.Fields.SdkMessageResponseId, Proxy.SdkMessageResponseField.Fields.SdkMessageResponseId, fields => fields
                                    .With.Alias("responsefield")
                                    .Select(cols => new { cols.Name, cols.Value, cols.Position, cols.PublicName, cols.ClrFormatter })
                                    .OrderByAsc(Proxy.SdkMessageResponseField.Fields.SdkMessageResponseFieldId))))))
                    .RetrieveAll();

            return records;
        }
     
        private IEnumerable<SdkMessage> ParseRecordsToSdkMessages(IEnumerable<Entity> records)
        {
            var sdkMessages = new List<SdkMessage>();

            foreach (var m in records.GroupBy(e => e.GetAttributeValue<Guid>("sdkmessageid")).ToArray())
            {
                var id = m.FirstOrDefault().Id;
                var name = m.FirstOrDefault().GetAttributeValue<string>("name");
                var isPrivate = m.FirstOrDefault().GetAttributeValue<bool?>("isprivate") == true;
                var isManaged = m.FirstOrDefault().GetAttributeValue<bool?>("ismanaged") == true;

                var msg = new SdkMessage(id, name, isPrivate, !isManaged);

                var filters = m
                    .GroupBy(e => e.GetAliasedValue<Guid>("filter.sdkmessagefilterid"))
                    .Select(e => new
                    {
                        filterId = e.FirstOrDefault().GetAliasedValue<Guid>("filter.sdkmessagefilterid"),
                        primaryObjectType = e.FirstOrDefault().GetAliasedValue<string>("filter.primaryobjecttypecode"),
                        secondaryObjectType = e.FirstOrDefault().GetAliasedValue<string>("filter.secondaryobjecttypecode"),
                        isVisible = e.FirstOrDefault().GetAliasedValue<bool?>("filter.isvisible") == true
                    })
                    .ToArray();

                foreach (var f in filters)
                {
                    msg.MessageFilters.Add(f.filterId, new SdkMessageFilter(f.filterId, f.primaryObjectType, f.secondaryObjectType, f.isVisible));
                }

                var pairs = m
                    .GroupBy(e => e.GetAliasedValue<Guid>("pair.sdkmessagepairid"))
                    .Select(e => new
                    {
                        Id = e.FirstOrDefault().GetAliasedValue<Guid>("pair.sdkmessagepairid"),
                        Namespace = e.FirstOrDefault().GetAliasedValue<string>("pair.namespace"),
                        Endpoint = e.FirstOrDefault().GetAliasedValue<string>("pair.endpoint"),
                        Request = e
                            .Where(req => req.Attributes.ContainsKey("request.sdkmessagerequestid") && req.Attributes["request.sdkmessagerequestid"] != null)
                            .GroupBy(req => req.GetAliasedValue<Guid>("request.sdkmessagerequestid"))
                            .Select(req => new
                            {
                                Id = req.FirstOrDefault().GetAliasedValue<Guid>("request.sdkmessagerequestid"),
                                Name = req.FirstOrDefault().GetAliasedValue<string>("request.name"),
                                Fields = req.GroupBy(field => field.GetAliasedValue<string>("requestfield.name"))
                                    .Select(field => new
                                    {
                                        Index = field.FirstOrDefault().GetAliasedValue<int>("requestfield.position"),
                                        Name = field.FirstOrDefault().GetAliasedValue<string>("requestfield.name"),
                                        ClrParser = field.FirstOrDefault().GetAliasedValue<string>("requestfield.clrparser"),
                                        IsOptional = field.FirstOrDefault().GetAliasedValue<bool?>("requestfield.isoptional").GetValueOrDefault()
                                    }).ToArray()
                            }).FirstOrDefault(),
                        Response = e
                            .Where(resp => resp.Attributes.ContainsKey("response.sdkmessageresponseid") && resp.Attributes["response.sdkmessageresponseid"] != null)
                            .GroupBy(resp => resp.GetAliasedValue<Guid>("response.sdkmessageresponseid"))
                            .Select(resp => new
                            {
                                Id = resp.FirstOrDefault().GetAliasedValue<Guid>("response.sdkmessageresponseid"),
                                Fields = resp.GroupBy(field => field.GetAliasedValue<string>("responsefield.name"))
                                    .Select(field => new
                                    {
                                        Index = field.FirstOrDefault().GetAliasedValue<int>("responsefield.position"),
                                        Name = field.FirstOrDefault().GetAliasedValue<string>("responsefield.name"),
                                        Value = field.FirstOrDefault().GetAliasedValue<string>("responsefield.value"),
                                        ClrFormatter = field.FirstOrDefault().GetAliasedValue<string>("responsefield.clrformatter")
                                    }).ToArray()
                            }).FirstOrDefault()
                    }).ToArray();

                foreach (var p in pairs)
                {
                    var messagePair = new SdkMessagePair(msg, p.Id, p.Namespace);

                    var req = p.Request;
                    var messageRequest = new SdkMessageRequest(messagePair, req.Id, req.Name);
                    foreach (var f in req.Fields)
                    {
                        var field = new SdkMessageRequestField(messageRequest, f.Index, f.Name, f.ClrParser, f.IsOptional);
                        messageRequest.RequestFields.Add(f.Index, field);
                    }

                    var resp = p.Response;
                    var messageResponse = new SdkMessageResponse(resp.Id);
                    foreach (var f in resp.Fields)
                    {
                        var field = new SdkMessageResponseField(f.Index, f.Name, f.ClrFormatter, f.Value);
                        messageResponse.ResponseFields.Add(f.Index, field);
                    }

                    messagePair.Request = messageRequest;
                    messagePair.Response = messageResponse;

                    msg.MessagePairs.Add(p.Id, messagePair);
                }

                sdkMessages.Add(msg);
            }

            return sdkMessages;

        }
    }
}
