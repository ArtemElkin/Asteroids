using System;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Core.Player;
using _Project.Features.UI.Common.Events;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.Gameplay.GameOverMenu
{
    public class PauseScreenViewModel : IDisposable
    {
        [Data("Title")]
        public readonly ReactiveProperty<string> Title = new();
        [Data("Score")]
        public readonly ReactiveProperty<string> Score = new();
        [Data("MaxScore")]
        public readonly ReactiveProperty<string> MaxScore = new();
        [Data("Active")]
        public readonly ReactiveProperty<bool> Active = new();
        [Data("IsGameOver")]
        public readonly ReactiveProperty<bool> IsGameOver = new();

        private readonly PlayerModel _playerModel;
        private readonly IGameStateService _gameStateService;
        private readonly IEventBus _eventBus;


        public PauseScreenViewModel(PlayerModel playerModel, IGameStateService gameStateService, IEventBus eventBus)
        {
            _playerModel = playerModel;
            _gameStateService = gameStateService;
            _eventBus = eventBus;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
            OnCurrentScoreChanged(_playerModel.CurrentScore);
            OnMaxScoreChanged(_playerModel.MaxScore);
            _playerModel.CurrentScoreChanged += OnCurrentScoreChanged;
            _playerModel.MaxScoreChanged += OnMaxScoreChanged;
            Active.Value = false;
            IsGameOver.Value = false;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Initialize:
                    Active.Value = false;
                    break;
                case GameState.Paused:
                    Title.Value = "Pause";
                    IsGameOver.Value = false;
                    Active.Value = true;
                    break;
                case GameState.GameOver:
                    Title.Value = "Game Over";
                    IsGameOver.Value = true;
                    Active.Value = true;
                    break;
                case GameState.Running:
                    Active.Value = false;
                    break;  
            }
        }
        
        [Method("OnResumeClick")]
        public void OnResumeClicked()
        {
            _gameStateService.SetState(GameState.Running);
        }

        [Method("OnRestartClick")]
        public void OnRestartClicked()
        {
            _gameStateService.SetState(GameState.Restart);
        }

        [Method("OnMainMenuClick")]
        public void OnMainMenuClicked()
        {
            _eventBus.Publish<MainMenuClickedEvent>();
        }

        private void OnCurrentScoreChanged(int newScore) => Score.Value = newScore.ToString();

        private void OnMaxScoreChanged(int newMaxScore) => MaxScore.Value = newMaxScore.ToString();

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
            _playerModel.CurrentScoreChanged -= OnCurrentScoreChanged;
            _playerModel.MaxScoreChanged -= OnMaxScoreChanged;
        }
    }
}