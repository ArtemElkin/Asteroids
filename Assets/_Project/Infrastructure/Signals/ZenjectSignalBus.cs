using System;
using _Project.Core.Signals;

namespace _Project.Infrastructure.Signals
{
    public class ZenjectSignalBus : ISignalBus
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
        
        public void Fire<TSignal>() => 
            _signalBus.Fire<TSignal>();

        public void Fire<TSignal>(TSignal signal) => 
            _signalBus.Fire(signal);
    }
}