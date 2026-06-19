using _Project.Core.Render.VFX;
using _Project.Features.Common.Hit;
using _Project.Infrastructure.Render;
using _Project.Infrastructure.Render.VFX;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories.VFX
{
    public class HitEffectFactory : AbstractFactory<HitInfo, IEffect, ParticleSystemEffect>
    {
        public HitEffectFactory(IInstantiator instantiator, 
            ParticleSystemEffect prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform)
        {
        }

        public override IEffect Create(HitInfo data)
        {
            var effect = _pool.Get();
            effect.transform.position = data.hitPosition.ToUnity();
            return effect;
        }
    }
}