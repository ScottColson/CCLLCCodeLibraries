using System;

namespace CCLLC.CDS.Sdk
{    
    public class PluginEventRegistration : IPluginEventRegistration
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
        public string EntityName { get; set; }
        /// <summary>
        /// Name of the message that the plugin should be triggered off of.
        /// </summary>
        public string MessageName { get; set; }
        /// <summary>
        /// Method that should be executed when the conditions of the Plugin Event have been met.
        /// </summary>
        public Action<ICDSPluginExecutionContext> PluginAction  { get; set; }

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            this.PluginAction.Invoke(executionContext);
        }
    }
}
