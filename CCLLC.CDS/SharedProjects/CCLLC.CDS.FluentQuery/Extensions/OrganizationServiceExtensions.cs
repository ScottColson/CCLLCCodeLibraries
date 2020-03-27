using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.FluentQuery
{
    public static class OrganizationServiceExtensions
    {
        public static IExecutableFluentQuery<E> Query<E>(this IOrganizationService organizationService) where E : Entity, new()
        {
            var query = new ExecutableFluentQuery<E>(organizationService);
            return query;
        }

        
    }
}
