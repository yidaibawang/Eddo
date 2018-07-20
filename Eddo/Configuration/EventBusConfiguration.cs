using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Configuration
{
    public class EventBusConfiguration : IEventBusConfiguration
    {
        public EventBusConfiguration()
        {
            UseDefaultEventBus = true;
        }
        public bool UseDefaultEventBus
        {
            get;set;
        }
    }
}
