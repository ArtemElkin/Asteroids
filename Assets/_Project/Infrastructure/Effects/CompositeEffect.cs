using System;
using System.Collections.Generic;
using _Project.Core.Render.VFX;
using UnityEngine;

namespace _Project.Infrastructure.Effects
{
    public class CompositeEffect : MonoBehaviour, IEffect
    {
        private List<IEffect> _effects;
        public bool IsPlaying { get; private set; }
        public event Action OnEnded;


        private void Awake()
        {
            _effects = new List<IEffect>(GetComponentsInChildren<IEffect>());
            _effects.Remove(this);
            foreach (var effect in _effects)
            {
                effect.OnEnded += OnChildEffectEnded;
            }
        }

        private void OnChildEffectEnded()
        {
            bool allEnded = true;
            foreach (var effect in _effects)
            {
                if (effect.IsPlaying)
                {
                    allEnded = false;
                    break;
                }
            }

            if (allEnded)
            {
                IsPlaying = false;
                OnEnded?.Invoke();
            }
        }

        public void Play()
        {
            IsPlaying = true;
            foreach (var effect in _effects)
            {
                effect.Play();
            }
        }

        public void Pause()
        {
            if (!IsPlaying) return;
            
            foreach (var effect in _effects)
            {
                effect.Pause();
            }
        }

        public void Resume()
        {
            if (!IsPlaying) return;
            
            foreach (var effect in _effects)
            {
                effect.Resume();
            }
        }

        public void Stop()
        {
            foreach (var effect in _effects)
            {
                effect.Stop();
            }
            IsPlaying = false;
        }

        private void OnDestroy()
        {
            foreach (var effect in _effects)
            {
                effect.OnEnded -= OnChildEffectEnded;
            }
        }
    }
}