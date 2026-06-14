using _Project.Features.Common.Effect;
using UnityEngine;

namespace _Project.Infrastructure.Render
{
    public class ParticleSystemEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        
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