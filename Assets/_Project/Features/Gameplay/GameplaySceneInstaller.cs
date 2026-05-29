using _Project.Core.Tools;
using _Project.Features.Gameplay.Ads;
using _Project.Features.Gameplay.Signals;
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
            BindGameplayStarter();
        }
        
        private void BindScreenService(Camera mainCamera)
        {
            Container
                .BindInterfacesAndSelfTo<ScreenService>()
                .AsSingle()
                .WithArguments(mainCamera);
        }

        private void BindPositionGenerator()
        {
            Container
                .BindInterfacesAndSelfTo<PositionGenerator>()
                .AsSingle();
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