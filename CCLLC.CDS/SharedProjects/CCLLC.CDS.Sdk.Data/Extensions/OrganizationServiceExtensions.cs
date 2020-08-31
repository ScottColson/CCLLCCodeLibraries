using System;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public static partial class Extensions
    {
        /// <summary>
        /// Execute a Fluent Query.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="organizationService"></param>
        /// <returns></returns>
        public static IExecutableFluentQuery<E> Query<E>(this IOrganizationService organizationService) where E : Entity, new()
        {
            var query = new ExecutableFluentQuery<E>(organizationService);
            return query;
        }

        /// <summary>
        /// Retrieve a single early bound record with columns selected through projection.
        /// </summary>        
        public static T GetRecord<T>(this IOrganizationService organizationService, EntityReference recordId, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            var columns = anonymousTypeInitializer.GetAttributeNamesArray<T>();

            return organizationService.GetRecord<T>(recordId, columns);
        }

        /// <summary>
        /// Retrieve a single early bound record with columns identified as string parameters.
        /// </summary> 
        public static T GetRecord<T>(this IOrganizationService organizationService, EntityReference recordId, params string[] columns) where T : Entity
        {
            return organizationService.GetRecord(recordId, columns).ToEntity<T>();
        }

        /// <summary>
        /// Retrieve an entity record with columns identified as string parameters.
        /// </summary> 
        public static Entity GetRecord(this IOrganizationService organizationService, EntityReference recordId, params string[] columns)
        {
            var columnSet = (columns == null || columns.Length == 0) ? new ColumnSet(true) : new ColumnSet(columns);

            var record = organizationService.Retrieve(
                recordId.LogicalName,
                recordId.Id,
                columnSet);

            return record;
        }

    }
}
