using _Project.Core.Render.VFX;
using UnityEngine;

namespace _Project.Infrastructure.Render.VFX
{
    public class ParticleSystemEffect : MonoBehaviour, IEffect
    {
        [SerializeField] protected ParticleSystem _particleSystem;

        
        public void Play()
        {
            _particleSystem.Play();
        }

        public void Stop()
        {
            _particleSystem.Stop();
            _particleSystem.Clear();
        }
    }
}