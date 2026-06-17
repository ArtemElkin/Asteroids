using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Features.Common.Collision;
using _Project.Features.Common.Effect;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;
using _Project.Infrastructure.Factories.VFX;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class VFXInstaller : MonoInstaller
    {
        [SerializeField] private ParticleSystemEffect _collisionEffect;
        [SerializeField] private ParticleSystemEffect _hitEffect;
        [SerializeField] private Transform _vfxParentTransform;
        
        
        public override void InstallBindings()
        {
            BindCollisionEffectFactory(_collisionEffect, _vfxParentTransform);
            BindCollisionEffectSpawner();
            
            BindHitEffectFactory(_hitEffect, _vfxParentTransform);
            BindHitEffectSpawner();
        }

        private void BindCollisionEffectFactory(ParticleSystemEffect effect, Transform parentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<CollisionData, IEffect>>()
                .To<CollisionEffectFactory>()
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
        
        private void BindHitEffectFactory(ParticleSystemEffect effect, Transform parentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<HitInfo, IEffect>>()
                .To<HitEffectFactory>()
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