using _Project.Core.Config;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Infrastructure.UnityServices;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTimeService();
            BindTimer();
            BindRandomService();
            BindSaveService();
            BindConfigProvider();
            BindSceneLoadService();
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

        private void BindTimer()
        {
            Container
                .BindInterfacesAndSelfTo<Timer>()
                .AsTransient();
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

        private void BindConfigProvider()
        {
            Container
                .Bind<IConfigProvider>()
                .To<ResourcesConfigProvider>()
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
    }
}