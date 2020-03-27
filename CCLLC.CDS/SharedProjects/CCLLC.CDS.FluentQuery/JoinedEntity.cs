using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public class JoinedEntity<P, E> : QueryEntity<P, E>, IJoinedEntity<P, E> where P : IQueryEntity where E : Entity
    {
        protected string Alias { get; private set; }
        protected JoinOperator JoinOperator { get; }

        public JoinedEntity(JoinOperator joinOperator,IQueryEntity parent) : base(parent) 
        {
            this.JoinOperator = joinOperator;
        }

        public IJoinedEntity<P, E> WithAlias(string aliasName)
        {
            this.Alias = aliasName;
            return this;
        }
    }
}
