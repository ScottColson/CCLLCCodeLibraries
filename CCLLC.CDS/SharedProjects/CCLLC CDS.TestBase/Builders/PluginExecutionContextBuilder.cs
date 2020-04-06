
using CCLLC.CDS.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Test.Builders
{
    public class PluginExecutionContextBuilder : DLaB.Xrm.Test.Builders.PluginExecutionContextBuilderBase<PluginExecutionContextBuilder>
    {
        protected override PluginExecutionContextBuilder This => this;


        #region Fluent Methods


        public PluginExecutionContextBuilder WithPluginEvent(PluginEventRegistration @event)
        {
            return WithRegisteredEvent((int)@event.Stage, @event.MessageName, @event.EntityName);
        }

        public PluginExecutionContextBuilder WithQuery(QueryExpression qry)
        {
            return this.WithInputParameter("Query", qry);
        }

        #endregion Fluent Methods
    }
}
