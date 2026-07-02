using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu.Settings
{
    public abstract class BaseSettingsPageViewModel : ISettingsPage
    {
        [Data("Active")]
        public readonly ReactiveProperty<bool> Active = new();
        

        public void Show()
        {
            Active.Value = true;
        }

        public void Hide()
        {
            Active.Value = false;
        }
    }
}