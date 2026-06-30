using _Project.Core.Factories;
using _Project.Core.Render.VFX;
using _Project.Infrastructure.Render.VFX;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Factories.VFX
{
    public class EffectFactory<TMarker> : AbstractFactory<Vector2, IEffect, CompositeEffect>, IEffectFactory<TMarker>
    {
        public EffectFactory(IInstantiator instantiator, 
            CompositeEffect prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform) { }

        public override IEffect Create(Vector2 position)
        {
            var effect = _pool.Get();
            effect.transform.position = position.ToUnity();
            return effect;
        }
    }
}