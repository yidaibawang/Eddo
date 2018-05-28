using System;

namespace Eddo.Domain.Entities.Auditing
{
    public abstract class CreationAuditedEntity<Tkey> : Entity<Tkey>, ICreationAudited
    {
        public virtual DateTime CreatedTime
        {
            get;set;
        }

        public virtual string CreateUserId
        {
            get;set;
        }
    }
}
