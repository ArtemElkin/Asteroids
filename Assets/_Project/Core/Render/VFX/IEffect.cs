using System;

namespace _Project.Core.Render.VFX
{
    public interface IEffect
    {
        void Play();
        void Pause();
        bool IsPlaying { get; }
        void Stop();
        event Action OnEnded;
    }
}