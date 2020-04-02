namespace CCLLC.CDS.Sdk
{
    public interface IPluginEventRegistration
    {
        string HandlerId { get; }
        ePluginStage Stage { get; }
        /// <summary>
        /// Logical name of the entity that the plugin should be registered against. Leave 'null' to register against all entities.
        /// </summary>
        string EntityName { get; }
        /// <summary>
        /// Name of the message that the plugin should be triggered off of.
        /// </summary>
        string MessageName { get; }

        void Invoke(ICDSPluginExecutionContext executionContext);
    }
}
