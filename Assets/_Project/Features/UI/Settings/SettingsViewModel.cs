using System;
using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Features.UI.Common.Events;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.Settings
{
    public class SettingsViewModel : IDisposable
    {
        [Data("Active")]
        public readonly ReactiveProperty<bool> Active = new();
        private readonly IEventBus _eventBus;
        private readonly SettingsCoordinator _coordinator;
        

        public SettingsViewModel(SettingsCoordinator coordinator, IEventBus eventBus)
        {
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
            _eventBus.Publish<BackToMenuClickedEvent>();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SettingsClickedEvent>(Show);
        }
    }
}