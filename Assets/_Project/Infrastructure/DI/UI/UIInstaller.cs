using _Project.Features.UI.Common.Binders;
using _Project.Features.UI.HUD;
using _Project.Features.UI.HUD.Binders;
using Plugins.MVVM;
using Zenject;

namespace _Project.Infrastructure.DI.UI
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
            
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<HpBinder>();
        }
    }
}