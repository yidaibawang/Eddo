namespace Eddo.Events.Entities
{
    public class EntityCreatingEventData<TEntity> :EntityChangingEventData<TEntity>
    {
        public EntityCreatingEventData(TEntity entity)
           : base(entity)
        {

        }
    }
}
