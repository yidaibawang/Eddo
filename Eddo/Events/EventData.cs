using System;

namespace Eddo.Events
{
    public  class EventData:IEventData
    {
        /// <summary>
        /// 事件时间
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 事件数据信息
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}
