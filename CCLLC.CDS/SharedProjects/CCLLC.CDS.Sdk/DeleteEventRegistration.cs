using Microsoft.Xrm.Sdk;
using System;

namespace CCLLC.CDS.Sdk
{
    public class DeleteEventRegistration<E> : IPluginEventRegistration where E : Entity, new()
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

        public Action<ICDSPluginExecutionContext, EntityReference> PluginAction { get; set; }

        public DeleteEventRegistration()
        {
            EntityName = new E().LogicalName;
            MessageName = MessageNames.Delete;
        }

        public void Invoke(ICDSPluginExecutionContext executionContext)
        {
            var target = executionContext.TargetEntity.ToEntityReference();
            PluginAction.Invoke(executionContext, target);
        }
    }
}
