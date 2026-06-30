namespace _Project.Core.Render.VFX
{
    public interface IEffect
    {
        void Play();
        void Pause();
        bool IsPaused { get; }
        void Stop();
    }
}