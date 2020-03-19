using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Test.Builders
{
    public abstract class EntityBuilder<TEntity> : DLaB.Xrm.Test.Builders.EntityBuilder<TEntity> where TEntity : Entity
    {       
    }
}
