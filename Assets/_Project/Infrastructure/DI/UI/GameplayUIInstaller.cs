using _Project.Features.UI.Common.Binders;
using _Project.Features.UI.Gameplay.GameOverMenu;
using _Project.Features.UI.Gameplay.HUD;
using _Project.Features.UI.Gameplay.HUD.Binders;
using Plugins.MVVM;
using Zenject;

namespace _Project.Infrastructure.DI.UI
{
    public class GameplayUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindHud();
            BindGameOverMenu();
        }

        private void BindHud()
        {
            Container
                .BindInterfacesAndSelfTo<HudViewModel>()
                .AsSingle()
                .NonLazy();

            BinderFactory.RegisterBinder<HpBinder>();
        }

        private void BindGameOverMenu()
        {
            Container
                .BindInterfacesAndSelfTo<PauseScreenViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}