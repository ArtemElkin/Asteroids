using _Project.Features.Common.Settings;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class SettingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSettingsModel();
            BindSettingsSaveController();
        }

        private void BindSettingsModel()
        {
            Container
                .Bind<SettingsModel>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsSaveController()
        {
            Container
                .BindInterfacesAndSelfTo<SettingsSaveController>()
                .AsSingle()
                .NonLazy();
        }
    }
}