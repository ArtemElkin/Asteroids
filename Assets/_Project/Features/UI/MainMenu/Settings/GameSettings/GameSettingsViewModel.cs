using _Project.Features.Common.Collision;
using _Project.Features.Common.Settings;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu.Settings.GameSettings
{
    public class GameSettingsViewModel : BaseSettingsPageViewModel
    {
        [Data("IsElasticSelected")]
        public ReactiveProperty<bool> IsElasticSelected = new();
        [Data("IsSimpleReflectionSelected")]
        public ReactiveProperty<bool> IsSimpleReflectionSelected = new();
        private readonly SettingsModel _settingsModel;


        public GameSettingsViewModel(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            
            IsSimpleReflectionSelected.Value =
                settingsModel.CollisionResolverType is CollisionResolverType.SimpleReflection;
            IsElasticSelected.Value = !IsSimpleReflectionSelected.Value;
        }

        [Method("OnElasticClick")]
        public void OnElasticClicked()
        {
            IsElasticSelected.Value = true;
            IsSimpleReflectionSelected.Value = false;
            _settingsModel.SetCollisionResolver(CollisionResolverType.Elastic);
        }
        [Method("OnSimpleReflectionClick")]
        public void OnSimpleReflectionClicked()
        {
            IsSimpleReflectionSelected.Value = true;
            IsElasticSelected.Value = false;
            _settingsModel.SetCollisionResolver(CollisionResolverType.SimpleReflection);
        }
    }
}