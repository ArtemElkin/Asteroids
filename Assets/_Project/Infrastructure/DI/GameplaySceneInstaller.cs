using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Common;
using _Project.Features.Common.Ads;
using _Project.Infrastructure.Lifecycle;
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
            BindPositionGenerator();
            BindGameplayAdsController();
            BindSpawnTimer();
            BindGameplayStarter();
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
                .BindInterfacesAndSelfTo<ElasticCollisionService>()
                .AsSingle();
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

        private void BindSpawnTimer()
        {
            Container
                .BindInterfacesAndSelfTo<SpawnTimer>()
                .AsTransient();
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