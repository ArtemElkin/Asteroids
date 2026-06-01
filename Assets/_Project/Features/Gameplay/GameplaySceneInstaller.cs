using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Ads;
using _Project.Features.Gameplay.Bounds;
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
            BindScreenService(_camera);
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