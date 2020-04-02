using Microsoft.Xrm.Sdk;
using System;

namespace CCLLC.CDS.Sdk
{
    public class UpdateEventRegistration<E> : IPluginEventRegistration where E : Entity, new()
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

        public Action<ICDSPluginExecutionContext, E> PluginAction { get; set; }

        public UpdateEventRegistration()
        {
            EntityName = new E().LogicalName;
            MessageName = MessageNames.Update;
        }

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            E target = executionContext.TargetEntity.ToEntity<E>();
            PluginAction.Invoke(executionContext, target);
        }
    }
}
