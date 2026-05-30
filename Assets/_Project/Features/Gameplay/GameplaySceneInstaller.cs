using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Ads;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Lifecycle;
using _Project.Infrastructure.Services;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        
        public override void InstallBindings()
        {
            BindMovementModel();
            BindScreenService(_camera);
            BindBoundsChecker();
            BindPositionGenerator();
            BindGameplayAdsController();
            BindGameplayStarter();
        }

        private void BindMovementModel()
        {
            Container
                .BindInterfacesAndSelfTo<MovementModel>()
                .AsTransient();
        }
        
        private void BindScreenService(Camera mainCamera)
        {
            Container
                .Bind<IScreenService>()
                .To<ScreenService>()
                .AsSingle()
                .WithArguments(mainCamera)
                .NonLazy();
        }

        private void BindBoundsChecker()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsChecker>()
                .AsTransient();
        }

        private void BindPositionGenerator()
        {
            Container
                .Bind<PositionGenerator>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindGameplayAdsController()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayAdsController>()
                .AsSingle();
        }

        private void BindGameplayStarter()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayStarter>()
                .FromNewComponentOn(gameObject)
                .AsSingle()
                .NonLazy();
        }
    }
}