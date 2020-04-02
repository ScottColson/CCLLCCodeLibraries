using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.Sdk
{
    public class QueryEventRegistration<E> : IPluginEventRegistration where E : Entity, new()
    {
        private string handlerId;
        /// <summary>
        /// Identifying name for the handler. Used in logging events.
        /// </summary>
        public string HandlerId
        {
            get { return handlerId ?? string.Empty; }
            set { handlerId = value; }
        }

        /// <summary>
        /// Execution pipeline stage that the plugin should be registered against.
        /// </summary>
        public ePluginStage Stage { get; set; }
        /// <summary>
        /// Logical name of the entity that the plugin should be registered against. Leave 'null' to register against all entities.
        /// </summary>
        public string EntityName { get; }
        /// <summary>
        /// Name of the message that the plugin should be triggered off of.
        /// </summary>
        public string MessageName { get; }

        public Action<ICDSPluginExecutionContext, QueryExpression, EntityCollection> PluginAction { get; set; }

        public QueryEventRegistration()
        {
            EntityName = new E().LogicalName;
            MessageName = MessageNames.RetrieveMultiple;
        }

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            _ = executionContext.InputParameters["Query"] ?? 
                throw new ArgumentNullException("RetrieveMultiple is missing required Query input parameter.");
            
            if(executionContext.InputParameters["Query"] is FetchExpression)
            {
                // Convert FetchXML to query expression.
                var fetchExpression = executionContext.InputParameters["Query"] as FetchExpression;

                var conversionRequest = new FetchXmlToQueryExpressionRequest
                {
                    FetchXml = fetchExpression.Query
                };

                var conversionResponse = (FetchXmlToQueryExpressionResponse)executionContext.OrganizationService.Execute(conversionRequest);

                executionContext.InputParameters["Query"] = conversionResponse.Query;
            }

            var qryExpression = executionContext.InputParameters["Query"] as QueryExpression;

           
            if (Stage == ePluginStage.PostOperation) 
            {
                var response = (executionContext.OutputParameters["EntityCollection"] as EntityCollection) ??
                    throw new ArgumentNullException("PostOp RetrieveMultiple Message is missing required EntityCollection output parameter.");
                PluginAction.Invoke(executionContext, qryExpression, response);
                executionContext.OutputParameters["EntityColection"] = response;
            }
            else
            {
                PluginAction.Invoke(executionContext, qryExpression, null);
            }          
        }
    }
}
