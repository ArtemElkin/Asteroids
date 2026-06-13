using System;

namespace _Project.Core.EventBus
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action callback) where TEvent : IEvent;
        void Subscribe<TEvent>(Action<TEvent> callback)  where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action callback) where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> callback)  where TEvent : IEvent;
        void Publish<TEvent>() where TEvent : IEvent;
        void Publish<TEvent>(TEvent signal) where TEvent : IEvent;
    }
}