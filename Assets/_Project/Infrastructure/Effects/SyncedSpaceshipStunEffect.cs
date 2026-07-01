using System;
using System.Collections.Generic;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Infrastructure.Render;

namespace _Project.Infrastructure.Effects
{
    public class SyncedSpaceshipStunEffect : IEffect
    {
        private readonly List<IEffect> _cloneEffects;
        private readonly IEffect _originEffect;
        private readonly IScreenWrapCloneSet _cloneSet;
        private readonly Storage<IEffect> _effectStorage;
        public bool IsPlaying { get; private set; }
        public event Action OnEnded;


        public SyncedSpaceshipStunEffect(IEffect originEffect, IScreenWrapCloneSet cloneSet, Storage<IEffect> effectStorage)
        {
            _originEffect = originEffect;
            _cloneSet = cloneSet;
            _cloneEffects = new List<IEffect>();
            _effectStorage = effectStorage;
            _effectStorage.Add(_originEffect);
        }
        public void Play()
        {
            _originEffect.Play();
            
            if (_cloneEffects.Count == 0) SyncClones();
            
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Play();
            }
            IsPlaying = true;
        }

        public void Pause()
        {
            if (!IsPlaying) return;
            
            _originEffect.Pause();
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Pause();
            }
        }

        public void Stop()
        {
            IsPlaying = false;
            _originEffect.Stop();
            _effectStorage.Remove(_originEffect);
            
            // if (_cloneEffects.Count == 0) SyncClones();
            
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Stop();
                _effectStorage.Remove(cloneEffect);
            }
            OnEnded?.Invoke();
        }
        
        private void SyncClones()
        {
            foreach (var cloneDrawable in _cloneSet.ClonesDrawables)
            {
                TransformView view = (TransformView)cloneDrawable;
                IEffect cloneStunEffect = view.GetComponentInChildren<IEffect>();
                _cloneEffects.Add(cloneStunEffect);
                _effectStorage.Add(cloneStunEffect);
            }
        }
    }
}