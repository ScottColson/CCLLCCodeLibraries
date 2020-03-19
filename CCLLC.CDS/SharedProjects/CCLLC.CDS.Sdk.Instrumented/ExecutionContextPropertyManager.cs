using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    /// <summary>
    /// Creates a list of telemetry properties based on the execution context information. This data
    /// provides additional meta information about logged telemetry.
    /// </summary>
    public class ExecutionContextPropertyManager : ICDSTelemetryPropertyManager
    {
        /// <summary>
        /// Create properties
        /// </summary>
        /// <param name="className">The name of the calling class.</param>
        /// <param name="executionContext">The current execution context.</param>
        /// <returns></returns>
        public virtual IDictionary<string, string> CreatePropertiesDictionary(string className, IExecutionContext executionContext)
        {
            var properties =  new Dictionary<string, string>{
                { "source", className },
                { "correlationId", executionContext.CorrelationId.ToString() },
                { "depth", executionContext.Depth.ToString() },
                { "initiatingUserId", executionContext.InitiatingUserId.ToString() },
                { "isInTransaction", executionContext.IsInTransaction.ToString() },
                { "isolationMode", executionContext.IsolationMode.ToString() },
                { "message", executionContext.MessageName },
                { "mode", getModeName(executionContext.Mode) },
                { "operationId", executionContext.OperationId.ToString() },
                { "orgId", executionContext.OrganizationId.ToString() },
                { "orgName", executionContext.OrganizationName },                
                { "requestId", executionContext.RequestId.ToString() },
                { "userId", executionContext.UserId.ToString() },
                { "entityId", executionContext.PrimaryEntityId.ToString() },
                { "entityName", executionContext.PrimaryEntityName }};

            //capture plugin execution context properties as telemetry context properties. 
            var asPluginExecutionContext = executionContext as IPluginExecutionContext;
            if (asPluginExecutionContext != null)
            {
                properties.Add("type", "Plugin");
                properties.Add("stage", getStageName(asPluginExecutionContext.Stage));                
            }           

            return properties;
        }

        protected string getModeName(int mode)
        {
            return mode == 0 ? "Synchronus" : "Asynchronus";
        }

        protected string getStageName(int stage)
        {
            switch (stage)
            {
                case 10:
                    return "Pre-validation";
                case 20:
                    return "Pre-operation";
                case 40:
                    return "Post-operation";
                default:
                    return "MainOperation";
            }
        }
    }
}
