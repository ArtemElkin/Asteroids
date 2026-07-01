namespace _Project.Core.Audio
{
    public interface IAudioService<in TSound>
    {
        void Play(TSound sound);
        void Pause();
        void Resume();
        void StopAll();
    }
}