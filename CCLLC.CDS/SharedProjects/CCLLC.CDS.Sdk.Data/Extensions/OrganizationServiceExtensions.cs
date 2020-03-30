using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public static partial class Extensions
    {
        public static IExecutableFluentQuery<E> Query<E>(this IOrganizationService organizationService) where E : Entity, new()
        {
            var query = new ExecutableFluentQuery<E>(organizationService);
            return query;
        }

        
    }
}
