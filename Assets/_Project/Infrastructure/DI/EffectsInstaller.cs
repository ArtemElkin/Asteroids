using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;
using _Project.Features.Common.Effects;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;
using _Project.Infrastructure.Effects;
using _Project.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField] private VisualEffect _collisionEffect;
        [SerializeField] private CompositeEffect _hitEffect;
        [SerializeField] private Transform _effectParentTransform;
        
        
        public override void InstallBindings()
        {
            BindEffectStorage();
            BindEffectPauseController();
            
            BindCollisionEffectFactory(_collisionEffect, _effectParentTransform);
            BindCollisionEffectSpawner();
            
            BindHitEffectFactory(_hitEffect, _effectParentTransform);
            BindHitEffectSpawner();
        }

        private void BindEffectStorage()
        {
            Container
                .Bind<Storage<IEffect>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEffectPauseController()
        {
            Container
                .Bind<EffectsManager>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCollisionEffectFactory(VisualEffect effect, Transform parentTransform)
        {
            Container
                .Bind<Core.Factories.IEffectFactory<CollisionData>>()
                .To<EffectFactory<CollisionData, VisualEffect>>()
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
                .To<EffectFactory<HitInfo, CompositeEffect>>()
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