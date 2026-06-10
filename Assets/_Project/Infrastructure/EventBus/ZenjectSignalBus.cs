using System;
using _Project.Core.EventBus;

namespace _Project.Infrastructure.Signals
{
    public class ZenjectSignalBus : IEventBus
    {
        private readonly Zenject.SignalBus _signalBus;
        
        
        public ZenjectSignalBus(Zenject.SignalBus signalBus) =>
            _signalBus = signalBus;

        public void Subscribe<TSignal>(Action callback) =>
            _signalBus.Subscribe<TSignal>(callback);
        
        public void Subscribe<TSignal>(Action<TSignal> callback) => 
            _signalBus.Subscribe(callback);
        
        public void Unsubscribe<TSignal>(Action callback) =>
            _signalBus.Unsubscribe<TSignal>(callback);
        
        public void Unsubscribe<TSignal>(Action<TSignal> callback) =>
            _signalBus.Unsubscribe(callback);
        
        public void Publish<TSignal>() => 
            _signalBus.Fire<TSignal>();

        public void Publish<TSignal>(TSignal signal) => 
            _signalBus.Fire(signal);
    }
}