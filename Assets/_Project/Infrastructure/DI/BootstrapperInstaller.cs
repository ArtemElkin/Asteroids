using _Project.Core.GameLifecycle;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class BootstrapperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper()
        {
            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle()
                .NonLazy();
        }
    }
}