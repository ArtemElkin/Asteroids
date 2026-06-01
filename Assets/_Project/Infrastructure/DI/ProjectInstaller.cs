using _Project.Core.Ads;
using _Project.Core.Config;
using _Project.Core.Player;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Infrastructure.Config;
using _Project.Infrastructure.Input;
using _Project.Infrastructure.Save;
using _Project.Infrastructure.Services;
using _Project.Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputHandlerPrefab;
        
        
        public override void InstallBindings()
        {
            BindSignalBus();
            Container.DeclareSignal<InitializeGameSignal>();
            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<StopGameSignal>();
            Container.DeclareSignal<GameRestartedSignal>();
            Container.DeclareSignal<StartGameClickedSignal>();
            Container.DeclareSignal<MenuClickedSignal>();

            BindTimeService();
            BindRandomService();
            BindSaveService();
            BindConfigProviders();
            BindPlayerModel();
            BindPlayerSaveController();
            BindInput();
            BindSceneLoadService();
            BindAdsService();
        }

        private void BindTimeService()
        {
            Container
                .Bind<ITimeService>()
                .To<UnityTimeService>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }

        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
            
            Container
                .Bind<ISignalBus>()
                .To<ZenjectSignalBus>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRandomService()
        {
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<PlayerPrefsSaveService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindConfigProviders()
        {
            Container
                .Bind<IConfigProvider>()
                .To<ResourcesConfigProvider>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerModel()
        {
            Container
                .Bind<PlayerModel>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerSaveController()
        {
            Container
                .Bind<PlayerSaveController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInput()
        {
            Container
                .BindInterfacesTo<StandaloneInputHandler>()
                .FromComponentInNewPrefab(_inputHandlerPrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoadService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAdsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockAdsService>()
                .AsSingle()
                .NonLazy();
#else
            // Container
            //     .BindInterfacesfTo<YandexAdsService>()
            //     .AsSingle()
            //     .NonLazy();
#endif
        }
    }
}