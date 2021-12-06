using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DotOS
{
    class MessageBus
    {
        private ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> _subscribers = new();
        public async Task SendTo<TReceiver>(IMessage message)
        {
            var messageType = message.GetType();
            var receiverType = typeof(TReceiver);

            var tasks = _subscribers
                .Where(s => s.Key.MessageType == messageType && s.Key.ReceiverType == receiverType)
                .Select(s => s.Value(message));

            await Task.WhenAll(tasks);
        }

        public void Receive<TMessage>(object receiver, Func<TMessage, Task> handler) where TMessage : IMessage
        {
            var sub = new MessageSubscriber(receiver.GetType(), typeof(TMessage));
            _subscribers.TryAdd(sub, (@event) => handler((TMessage)@event));
        }
    }
    record MessageSubscriber(Type ReceiverType, Type MessageType);
    interface IMessage { }
}