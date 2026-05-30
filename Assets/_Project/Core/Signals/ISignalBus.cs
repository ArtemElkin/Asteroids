using System;

namespace _Project.Core.Signals
{
    public interface ISignalBus
    {
        void Subscribe<TSignal>(Action callback);
        void Subscribe<TSignal>(Action<TSignal> callback);
        void Unsubscribe<TSignal>(Action callback);
        void Unsubscribe<TSignal>(Action<TSignal> callback);
        void Fire<TSignal>();
        void Fire<TSignal>(TSignal signal);
    }
}