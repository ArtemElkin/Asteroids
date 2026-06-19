using _Project.Core.Physics.Collision;
using _Project.Core.Render.VFX;
using _Project.Infrastructure.Render;
using _Project.Infrastructure.Render.VFX;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories.VFX
{
    public class CollisionEffectFactory : AbstractFactory<CollisionData, IEffect, ParticleSystemEffect>
    {
        public CollisionEffectFactory(IInstantiator instantiator, 
            ParticleSystemEffect prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform)
        {
        }

        public override IEffect Create(CollisionData data)
        {
            var effect = _pool.Get();
            effect.transform.position = data.contactPointPosition.ToUnity();
            return effect;
        }
    }
}