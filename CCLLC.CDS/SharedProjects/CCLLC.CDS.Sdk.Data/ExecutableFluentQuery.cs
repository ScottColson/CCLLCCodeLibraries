using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public class ExecutableFluentQuery<E> : FluentQuery<IExecutableFluentQuery<E>,E>, IExecutableFluentQuery<E> where E : Entity, new()
    {
        protected IOrganizationService OrganizationService { get; }      

        public ExecutableFluentQuery(IOrganizationService organizationService)
            : base()
        {
            this.OrganizationService = organizationService ?? throw new ArgumentNullException("organizationService");
        }

        /// <summary>
        /// Retrieve records using paging limits.
        /// </summary>
        /// <returns></returns>
        public IList<E> Retrieve()
        {
            var queryExpression = this.getQueryExpression();

            return OrganizationService.RetrieveMultiple(queryExpression).Entities
                .Select(e => e.ToEntity<E>()).ToList();
        }

        /// <summary>
        /// Retrieve records from all pages.
        /// </summary>
        /// <returns></returns>
        public IList<E> RetrieveAll()
        {
            var qryExpression = this.getQueryExpression();

            var allRecords = new List<E>();
            bool moreRecords = true;

            while(moreRecords)
            {
                var result = OrganizationService.RetrieveMultiple(qryExpression);
                moreRecords = result.MoreRecords;
                var records = result.Entities.Select(e => e.ToEntity<E>());
                allRecords.AddRange(records);

                if (moreRecords)
                {
                    qryExpression.PageInfo.PagingCookie = result.PagingCookie;
                }
            }          

            return allRecords;
        }

        /// <summary>
        /// Retrieve the first record that matches the query definition.
        /// </summary>
        /// <returns></returns>
        public E FirstOrDefault()
        {
            this.With.RecordLimit(1); //set the retrieve record limit to 1

            var queryExpression = this.getQueryExpression();

            return OrganizationService.RetrieveMultiple(queryExpression).Entities
                .Select(e => e.ToEntity<E>()).ToList().FirstOrDefault();            
        }
    }
}
