using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;
using _Project.Infrastructure.Factories.VFX;
using _Project.Infrastructure.Render.VFX;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField] private CompositeEffect _collisionEffect;
        [SerializeField] private CompositeEffect _hitEffect;
        [SerializeField] private Transform _effectParentTransform;
        
        
        public override void InstallBindings()
        {
            BindCollisionEffectFactory(_collisionEffect, _effectParentTransform);
            BindCollisionEffectSpawner();
            
            BindHitEffectFactory(_hitEffect, _effectParentTransform);
            BindHitEffectSpawner();
        }

        private void BindCollisionEffectFactory(CompositeEffect effect, Transform parentTransform)
        {
            Container
                .Bind<Core.Factories.IEffectFactory<CollisionData>>()
                .To<EffectFactory<CollisionData>>()
                .AsSingle()
                .WithArguments(effect, parentTransform)
                .NonLazy();
        }

        private void BindCollisionEffectSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<EffectSpawner<CollisionProcessedEvent, CollisionData>>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindHitEffectFactory(CompositeEffect effect, Transform parentTransform)
        {
            Container
                .Bind<Core.Factories.IEffectFactory<HitInfo>>()
                .To<EffectFactory<HitInfo>>()
                .AsSingle()
                .WithArguments(effect, parentTransform)
                .NonLazy();
        }

        private void BindHitEffectSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<EffectSpawner<HitEvent, HitInfo>>()
                .AsSingle()
                .NonLazy();
        }
    }
}