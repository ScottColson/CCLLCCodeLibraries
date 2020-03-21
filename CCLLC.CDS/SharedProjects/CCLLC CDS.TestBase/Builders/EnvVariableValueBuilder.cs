using DLaB.Xrm.Test;

namespace CCLLC.CDS.Test.Builders
{
    public class EnvVariableValueBuilder : EntityBuilder<TestProxy.EnvironmentVariableValue>
    {       
        public EnvVariableValueBuilder(Id id) 
            : base()
        {
            Id = id;
        }

        public EnvVariableValueBuilder ForDefinition(Id value)
        {
            Proxy.EnvironmentVariableDefinitionId = value;
            return this;
        }

        public EnvVariableValueBuilder WithValue(string value)
        {
            Proxy.Value = value;
            return this;
        }
    }
}
