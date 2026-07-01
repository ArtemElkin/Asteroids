using _Project.Core.Factories;
using _Project.Core.Render.VFX;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Factories
{
    public class EffectFactory<TMarker, TPrefab> : 
        AbstractFactory<Vector2, IEffect, TPrefab>, IEffectFactory<TMarker>
        where TPrefab: MonoBehaviour, IEffect
    {
        public EffectFactory(IInstantiator instantiator, 
            TPrefab prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform) { }

        public override IEffect Create(Vector2 position)
        {
            var effect = _pool.Get();
            effect.transform.position = position.ToUnity();
            return effect;
        }

        public void Release(IEffect entity)
        {
            var view = (TPrefab) entity;
            view.transform.position = Vector2.zero.ToUnity();
            _pool.Release(view);
        }
    }
}