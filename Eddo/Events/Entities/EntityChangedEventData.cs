using System;

namespace Eddo.Events.Entities
{
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityChangedEventData(TEntity entity) : base(entity)
        {
        }
    }
}
