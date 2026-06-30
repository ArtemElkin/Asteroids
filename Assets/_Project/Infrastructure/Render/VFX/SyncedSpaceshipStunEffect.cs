using System.Collections.Generic;
using _Project.Core.Render.VFX;
using _Project.Features.Common.ScreenWrapClone;

namespace _Project.Infrastructure.Render.VFX
{
    public class SyncedSpaceshipStunEffect : IEffect
    {
        private readonly List<IEffect> _cloneEffects;
        private readonly IEffect _originEffect;
        private readonly IScreenWrapCloneSet _cloneSet;
        public bool IsPaused { get; private set; }


        public SyncedSpaceshipStunEffect(IEffect originEffect, IScreenWrapCloneSet cloneSet)
        {
            _originEffect = originEffect;
            _cloneSet = cloneSet;
            _cloneEffects = new List<IEffect>();
        }
        public void Play()
        {
            if (IsPaused) IsPaused = false;
            _originEffect.Play();
            
            if (_cloneEffects.Count == 0) SyncClones();
            
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Play();
            }
        }

        public void Pause()
        {
            IsPaused = true;
            _originEffect.Pause();
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Pause();
            }
        }

        public void Stop()
        {
            _originEffect.Stop();
            
            if (_cloneEffects.Count == 0) SyncClones();
            
            foreach (var cloneEffect in _cloneEffects)
            {
                cloneEffect.Stop();
            }
            IsPaused = false;
        }

        private void SyncClones()
        {
            foreach (var cloneDrawable in _cloneSet.ClonesDrawables)
            {
                TransformView view = (TransformView)cloneDrawable;
                IEffect cloneStunEffect = view.GetComponentInChildren<IEffect>();
                _cloneEffects.Add(cloneStunEffect);
            }
        }
    }
}