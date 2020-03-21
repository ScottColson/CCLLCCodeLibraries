using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Builders;
using Microsoft.Xrm.Sdk;
using CCLLC.Core;

namespace CCLLC.CDS.Test
{
    public abstract class TestMethodClassBase : TestMethodClassBaseDLaB
    {
        protected IIocContainer Container { get; }

        public TestMethodClassBase() : base()
        {
            Container = new IocContainer();
        }

        protected override IAgnosticServiceBuilder GetOrganizationServiceBuilder(IOrganizationService service) { return new Builders.OrganizationServiceBuilder(service); }

        //protected IProcessExecutionContext GetExecutionContext(IOrganizationService service)
        //{
        //    return new CCLLC.CDS.Test.ExecutionContext.Context(service, this.Container);
        //}

        protected override void LoadConfigurationSettings()
        {
            TestInitializer.InitializeTestSettings();
        }

      
        public void Test()
        {
            Test(new DebugLogger());
        }
    }
}
