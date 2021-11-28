using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DotOS
{
    class EventBus
    {
        private ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>> _subscribers;
        public EventBus()
        {
            _subscribers = new ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>>();
        }
        public void Subscribe<T>(Func<T, Task> handler)
        {
            var sub = new EventSubscriber(typeof(T));
            _subscribers.TryAdd(sub, item => handler((T)item));
        }
        public async Task Publish<T>(T message) where T: IEvent
        {
            var messageType = typeof(T);

            var tasks = _subscribers
                .Where(x => x.Key.MessageType == messageType)
                .Select(x => x.Value(message));

            await Task.WhenAll(tasks);
        }
    }
    class EventSubscriber 
    {
        public EventSubscriber(Type msgType)
        {
            MessageType = msgType;
        }
        public Type MessageType { get; set; }
    }

    interface IEvent { }
}