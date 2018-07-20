using System;

namespace Eddo.Events.Entities
{
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">The entity which is deleted</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
