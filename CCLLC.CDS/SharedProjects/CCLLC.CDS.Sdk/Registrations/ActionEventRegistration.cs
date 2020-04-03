using Microsoft.Xrm.Sdk;
using System;

namespace CCLLC.CDS.Sdk
{
    public class ActionEventRegistration<TRequest,TResponse> : IPluginEventRegistration 
        where TRequest : OrganizationRequest, new()
        where TResponse : OrganizationResponse, new()
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

        public Action<ICDSPluginExecutionContext, TRequest, TResponse> PluginAction { get; set; }

        public ActionEventRegistration()
        {            
            EntityName = null;
            MessageName = new TRequest().RequestName;
        }

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            var request = new TRequest()
            {
                Parameters = executionContext.InputParameters
            }; 
        
            if (Stage == ePluginStage.PostOperation) 
            {
                var response = new TResponse()
                {
                    Results = executionContext.OutputParameters
                };

                PluginAction.Invoke(executionContext, request, response);                
            }
            else
            {
                PluginAction.Invoke(executionContext, request, null);
            }          
        }
    }
}
