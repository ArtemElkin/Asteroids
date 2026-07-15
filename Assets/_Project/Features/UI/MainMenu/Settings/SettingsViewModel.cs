using System;
using _Project.Core.EventBus;
using _Project.Features.Common.Settings;
using _Project.Features.UI.Common.Events;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu.Settings
{
    public class SettingsViewModel : IDisposable
    {
        [Data("Active")]
        public readonly ReactiveProperty<bool> Active = new();
        private readonly SettingsSaveController _settingsSaveController;
        private readonly SettingsCoordinator _coordinator;
        private readonly IEventBus _eventBus;
        

        public SettingsViewModel(
            SettingsSaveController settingsSaveController,
            SettingsCoordinator coordinator, 
            IEventBus eventBus)
        {
            _settingsSaveController = settingsSaveController;
            _coordinator = coordinator;
            _eventBus = eventBus;
            _eventBus.Subscribe<SettingsClickedEvent>(Show);
        }

        private void Show()
        {
            Active.Value = true;
        }

        private void Hide()
        {
            Active.Value = false;
        }
        
        [Method("OnNextPageClick")]
        public void OnNextPageClicked()
        {
            _coordinator.NextPage();
        }
        
        [Method("OnBackToMenuClick")]
        public void OnBackToMenuClicked()
        {
            Hide();
            _settingsSaveController.Save();
            _eventBus.Publish<BackToMenuClickedEvent>();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SettingsClickedEvent>(Show);
        }
    }
}