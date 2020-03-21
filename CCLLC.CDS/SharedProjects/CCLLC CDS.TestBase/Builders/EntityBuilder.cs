using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Test.Builders
{
    public abstract class EntityBuilder<TEntity> : DLaB.Xrm.Test.Builders.EntityBuilder<TEntity> where TEntity : Entity, new()
    {
        protected TEntity Proxy { get; set; }

        protected EntityBuilder()
        {
            Proxy = new TEntity();
        }

        protected override TEntity BuildInternal()
        {
            return Proxy;
        }
    }
}
