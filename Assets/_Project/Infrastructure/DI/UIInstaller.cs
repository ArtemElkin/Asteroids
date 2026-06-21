using _Project.Features.UI.HUD;
using Plugins.MVVM;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindHud();
        }

        private void BindHud()
        {
            Container
                .BindInterfacesAndSelfTo<HudViewModel>()
                .AsSingle()
                .NonLazy();
            
            BinderFactory.RegisterBinder<HudBinder>();
        }
    }
}