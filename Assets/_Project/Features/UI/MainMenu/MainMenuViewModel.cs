using System;
using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Features.UI.Common.Events;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu
{
    public class MainMenuViewModel : IDisposable
    {
        [Data("MaxScore")]
        public readonly ReactiveProperty<string> MaxScore = new();
        [Data("Active")]
        public readonly ReactiveProperty<bool> Active = new(true);
        private readonly IEventBus _eventBus;


        public MainMenuViewModel(PlayerModel playerModel, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<BackToMenuClickedEvent>(Show);
            OnMaxScoreChanged(playerModel.MaxScore);
        }

        private void Show()
        {
            Active.Value = true;
        }

        private void Hide()
        {
            Active.Value = false;
        }

        [Method("OnStartClick")]
        public void OnStartClicked()
        {
            _eventBus.Publish<StartGameClickedEvent>();
        }
        [Method("OnSettingsClick")]
        public void OnSettingsClicked()
        {
            Hide();
            _eventBus.Publish<SettingsClickedEvent>();
        }

        private void OnMaxScoreChanged(int newMaxScore) => MaxScore.Value = $"MAX SCORE:\n {newMaxScore}";

        public void Dispose()
        {
            _eventBus.Unsubscribe<BackToMenuClickedEvent>(Show);
        }
    }
}