using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class LaserBeamInstaller : MonoInstaller
    {
        [SerializeField] private TransformView _laserBeamPrefab;
        [SerializeField] private Transform _laserBeamParentTransform;
        
        public override void InstallBindings()
        {
            BindLaserBeamStorage();
            BindLaserBeamFactory(_laserBeamPrefab, _laserBeamParentTransform);
            BindLaserBeamDespawner();
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