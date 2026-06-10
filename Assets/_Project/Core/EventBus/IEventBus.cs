using System;

namespace _Project.Core.EventBus
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action callback);
        void Subscribe<TEvent>(Action<TEvent> callback);
        void Unsubscribe<TEvent>(Action callback);
        void Unsubscribe<TEvent>(Action<TEvent> callback);
        void Publish<TEvent>();
        void Publish<TEvent>(TEvent signal);
    }
}