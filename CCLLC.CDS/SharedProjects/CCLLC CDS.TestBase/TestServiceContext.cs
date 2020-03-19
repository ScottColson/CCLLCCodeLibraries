using Microsoft.Xrm.Sdk;

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace TestProxy
{


    public partial class TestServiceContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
    {
        public TestServiceContext(IOrganizationService service) : base(service)
        {
        }
    }
}
