namespace _Project.Core.Audio
{
    public interface IAudioService<in TSound>
    {
        void PlaySound(TSound sound);
        void PauseSound();
        void ResumeSound();
        void StopAllSounds();
    }
}