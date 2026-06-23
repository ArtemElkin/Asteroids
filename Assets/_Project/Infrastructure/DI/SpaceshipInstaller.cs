using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Projectile;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [Header("Spaceship")]
        [SerializeField] private MovableView _spaceshipPrefab;
        [SerializeField] private TransformView _spaceshipScreenWrapClonePrefab;
        [SerializeField] private Transform _spaceshipParentTransform;
        
        [Header("Projectile Weapon")]
        [SerializeField] private MovableView _projectilePrefab;
        [SerializeField] private Transform _projectileParentTransform;
        [Header("Laser Weapon")]
        
        [SerializeField] private TransformView _laserBeamPrefab;
        [SerializeField] private Transform _laserBeamParentTransform;
        
        
        public override void InstallBindings()
        {
            // Spaceship
            BindSpaceshipStorage();
            BindSpaceshipFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipSpawner();
            BindSpaceshipDespawner();
            
            BindSpaceshipCloneFactory(_spaceshipScreenWrapClonePrefab, _spaceshipParentTransform);
            
            // Projectile Weapon
            BindProjectileStorage();
            BindProjectileFactory(_projectilePrefab, _projectileParentTransform);
            BindProjectileDespawner();
            
            // Laser Weapon
            BindLaserBeamStorage();
            BindLaserBeamFactory(_laserBeamPrefab, _laserBeamParentTransform);
            BindLaserBeamDespawner();
        }

        private void BindSpaceshipStorage()
        {
            Container
                .Bind<Storage<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipFactory(
            MovableView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind(
                    typeof(Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>),
                    typeof(IReleaser<SpaceshipFacade>))
                .To<SpaceshipFactory>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }

        private void BindSpaceshipSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipCloneFactory(
            TransformView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind<Core.Factories.IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, SpaceshipFacade>>()
                .To<ScreenWrapCloneFactory<SpaceshipFacade>>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
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
        
        private void BindLaserBeamStorage()
        {
            Container
                .Bind<Storage<LaserBeamFacade>>()
                .AsSingle();
        }

        private void BindLaserBeamFactory(TransformView laserBeamPrefab, Transform laserBeamParentTransform)
        {
            Container
                .Bind(
                    typeof(Core.Factories.IFactory<LaserBeamSpawnData, LaserBeamFacade>),
                    typeof(IReleaser<LaserBeamFacade>))
                .To<LaserBeamFactory>()
                .AsSingle()
                .WithArguments(laserBeamPrefab, laserBeamParentTransform)
                .NonLazy();
        }

        private void BindLaserBeamDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<LaserBeamFacade>>()
                .AsSingle()
                .NonLazy();
        }
    }
}