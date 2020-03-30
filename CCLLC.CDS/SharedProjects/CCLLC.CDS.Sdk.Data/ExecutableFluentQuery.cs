using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public class ExecutableFluentQuery<E> : FluentQuery<IExecutableFluentQuery<E>,E>, IExecutableFluentQuery<E> where E : Entity, new()
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
            
        public E FirstOrDefault()
        {
            this.With.RecordLimit(1); //set the retrieve record limit to 1

            var queryExpression = this.getQueryExpression();

            return OrganizationService.RetrieveMultiple(queryExpression).Entities
                .Select(e => e.ToEntity<E>()).ToList().FirstOrDefault();            
        }
    }
}
