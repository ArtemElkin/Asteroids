using _Project.Features.UI.Gameplay.HUD;
using _Project.Features.UI.Gameplay.HUD.Binders;
using _Project.Features.UI.Gameplay.PauseScreen;
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

            BinderFactory.RegisterBinder<HealthBinder>();
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