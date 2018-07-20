using System;
using System.Threading.Tasks;
using System.Reflection;

namespace Eddo.Events
{
    public class NullEventBus : IEventBus
    {
        public static NullEventBus Instance { get { return SingletonInstance; } }
        private static readonly NullEventBus SingletonInstance = new NullEventBus();
        public void Register(Type eventType, Type handler)
        {

        }

        public void Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
    
        }

        public void Register<TEventData>(IEventHandler eventHandler) where TEventData : IEventData
        {
           
        }

        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
    
        }

        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
         
        }

        public void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
           
        }

        public Task TriggerAsycn<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {

            return new Task(() => { });
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {

            return new Task(() => { });
        }

        public void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData
        {
          
        }

        public void UnRegisterAll<TEventData>() where TEventData : IEventData
        {
      
        }

        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            
        }

        public Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            return new Task(() => { });
        }
    }
}
