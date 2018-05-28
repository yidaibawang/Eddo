﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Events.Entities
{
    public class EntityChangeReport
    {
        public List<EntityChangeEntry> ChangedEntities { get; }

        public List<DomainEventEntry> DomainEvents { get; }

        public EntityChangeReport()
        {
            ChangedEntities = new List<EntityChangeEntry>();
            DomainEvents = new List<DomainEventEntry>();
        }

        public bool IsEmpty()
        {
            return ChangedEntities.Count <= 0 && DomainEvents.Count <= 0;
        }

        public override string ToString()
        {
            return $"[EntityChangeReport] ChangedEntities: {ChangedEntities.Count}, DomainEvents: {DomainEvents.Count}";
        }
    }
}
