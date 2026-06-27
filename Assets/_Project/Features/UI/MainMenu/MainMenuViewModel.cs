using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Features.UI.Common.Events;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu
{
    public class MainMenuViewModel
    {
        [Data("MaxScore")]
        public readonly ReactiveProperty<string> MaxScore = new();
        private readonly IEventBus _eventBus;


        public MainMenuViewModel(PlayerModel playerModel, IEventBus eventBus)
        {
            _eventBus = eventBus;
            OnMaxScoreChanged(playerModel.MaxScore);
        }
        
        [Method("OnStartClick")]
        public void OnStartClicked()
        {
            _eventBus.Publish<StartGameClickedEvent>();
        }

        private void OnMaxScoreChanged(int newMaxScore) => MaxScore.Value = newMaxScore.ToString();
    }
}