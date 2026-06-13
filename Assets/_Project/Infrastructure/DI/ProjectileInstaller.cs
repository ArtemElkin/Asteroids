using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Spaceship.Weapon.Projectile;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class ProjectileInstaller : MonoInstaller
    {
        [SerializeField] private MovableView _projectilePrefab;
        [SerializeField] private Transform _projectileParentTransform;
        
        public override void InstallBindings()
        {
            BindProjectileStorage();
            BindProjectileFactory(_projectilePrefab, _projectileParentTransform);
            BindProjectileDespawner();
        }

        private void BindProjectileStorage()
        {
            Container
                .Bind<Storage<ProjectileFacade>>()
                .AsSingle();
        }

        private void BindProjectileFactory(MovableView projectileView, Transform projectileParentTransform)
        {
            Container
                .Bind(
                    typeof(Core.Factories.IFactory<ProjectileSpawnData, ProjectileFacade>),
                    typeof(IReleaser<ProjectileFacade>))
                .To<ProjectileFactory>()
                .AsSingle()
                .WithArguments(projectileView,  projectileParentTransform)
                .NonLazy();
        }

        private void BindProjectileDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<ProjectileFacade>>()
                .AsSingle()
                .NonLazy();
        }
    }
}