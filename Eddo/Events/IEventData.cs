using System;

namespace Eddo.Events
{
    public interface  IEventData
    {   
        /// <summary>
        /// 触发事件时间
        /// </summary>
        DateTime EventTime { get; set; }
        /// <summary>
        /// 触发事件数据源
        /// </summary>
        object EventSource { get; set; }
    }
}
