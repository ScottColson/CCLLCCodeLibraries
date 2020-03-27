using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.FluentQuery
{
    public class ExecutableFluentQuery<E> : FluentQuery<IExecutableFluentQuery,E>, IExecutableFluentQuery<E> where E : Entity, new()
    {
        public IOrganizationService OrganizationService { get; }      

        public ExecutableFluentQuery(IOrganizationService organizationService)
            : base()
        {
            this.OrganizationService = organizationService ?? throw new ArgumentNullException("organizationService");
        }



        public IList<E> Retreive()
        {
            var queryExpression = this.getQueryExpression();

            return OrganizationService.RetrieveMultiple(queryExpression).Entities
                .Select(e => e.ToEntity<E>()).ToList();
        }

        
    }
}
