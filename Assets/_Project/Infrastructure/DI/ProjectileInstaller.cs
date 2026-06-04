using _Project.Features.Projectile;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class ProjectileInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private Transform _projectileParentTransform;
        
        public override void InstallBindings()
        {
            BindProjectileFactory(_projectilePrefab, _projectileParentTransform);
            BindProjectileSpawner();
            BindProjectileDespawner();
        }

        private void BindProjectileFactory(ProjectileView projectileView, Transform projectileParentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<ProjectileSpawnData, ProjectileFacade>>()
                .To<ProjectileFactory>()
                .AsSingle()
                .WithArguments(projectileView,  projectileParentTransform)
                .NonLazy();
        }
        private void BindProjectileSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectileSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindProjectileDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectileDespawner>()
                .AsSingle()
                .NonLazy();
        }
    }
}