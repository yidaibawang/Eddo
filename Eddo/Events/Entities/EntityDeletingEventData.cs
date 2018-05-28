using System;

namespace Eddo.Events.Entities
{
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">The entity which is being deleted</param>
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
