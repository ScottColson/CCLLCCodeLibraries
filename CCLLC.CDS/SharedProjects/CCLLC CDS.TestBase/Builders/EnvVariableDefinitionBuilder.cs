using DLaB.Xrm.Test;

namespace CCLLC.CDS.Test.Builders
{
    public class EnvVariableDefinitionBuilder : EntityBuilder<TestProxy.EnvironmentVariableDefinition>
    {
     
        public EnvVariableDefinitionBuilder(Id id) 
            : base()
        {
            this.Id = id;
        }

        public EnvVariableDefinitionBuilder WithDisplayName(string value)
        {
            Proxy.DisplayName = value;
            return this;
        }

        public EnvVariableDefinitionBuilder WithDefaultValue(string value)
        {
            Proxy.DefaultValue = value;
            return this;
        }
    }

}
