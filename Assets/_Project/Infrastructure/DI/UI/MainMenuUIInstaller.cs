using _Project.Features.UI.MainMenu;
using _Project.Features.UI.Settings;
using _Project.Features.UI.Settings.GameSettings;
using _Project.Features.UI.Settings.VolumeSettings;
using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMainMenu();
        BindSettings();
    }

    private void BindMainMenu()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuViewModel>()
            .AsSingle()
            .NonLazy();
    }

    private void BindSettings()
    {
        Container
            .BindInterfacesAndSelfTo<SettingsViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<GameSettingsViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<VolumeSettingsViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<SettingsCoordinator>()
            .AsSingle()
            .NonLazy();
    }
}
