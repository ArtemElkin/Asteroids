using _Project.Core.GameLifecycle;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Ads;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Collision;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Infrastructure.GameLifecycle;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        
        public override void InstallBindings()
        {
            BindScreenService(_camera);
            
            BindCollisionService();

            BindGameStateService();
            
            BindPositionGenerator();
            BindGameplayStarter();
            BindPauseController();
            BindRestartController();
            
            BindGameplayAdsController();
            BindEnemyAwardsService();
            
            BindBoundsService();
            BindBoundsWarper();
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

        private void BindCollisionService()
        {
            Container
                // .Bind<ElasticCollisionService>()
                .Bind<SimpleReflectionCollisionService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameStateService()
        {
            Container
                .BindInterfacesAndSelfTo<GameStateService>()
                .AsSingle()
                .NonLazy();
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

        private void BindPauseController()
        {
            Container
                .BindInterfacesAndSelfTo<PauseController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRestartController()
        {
            Container
                .BindInterfacesAndSelfTo<RestartController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyAwardsService()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyAwardsService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindBoundsService()
        {
            Container
                .Bind<BoundsService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBoundsWarper()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsWarper>()
                .AsSingle()
                .NonLazy();
        }
    }
}