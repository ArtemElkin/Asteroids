using System;
using _Project.Core.EventBus;

namespace _Project.Infrastructure.EventBus
{
    public class ZenjectSignalBus : IEventBus
    {
        private readonly Zenject.SignalBus _signalBus;
        
        
        public ZenjectSignalBus(Zenject.SignalBus signalBus) =>
            _signalBus = signalBus;

        public void Subscribe<TEvent>(Action callback) where TEvent : IEvent =>
            _signalBus.Subscribe<TEvent>(callback);
        
        public void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent => 
            _signalBus.Subscribe(callback);
        
        public void Unsubscribe<TEvent>(Action callback) where TEvent : IEvent =>
            _signalBus.Unsubscribe<TEvent>(callback);
        
        public void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent =>
            _signalBus.Unsubscribe(callback);
        
        public void Publish<TEvent>() where TEvent : IEvent => 
            _signalBus.Fire<TEvent>();

        public void Publish<TEvent>(TEvent signal) where TEvent : IEvent => 
            _signalBus.Fire(signal);
    }
}