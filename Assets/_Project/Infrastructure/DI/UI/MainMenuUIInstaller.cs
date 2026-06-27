using _Project.Features.UI.MainMenu;
using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMainMenuViewModel();
    }

    private void BindMainMenuViewModel()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuViewModel>()
            .AsSingle()
            .NonLazy();
    }
}
