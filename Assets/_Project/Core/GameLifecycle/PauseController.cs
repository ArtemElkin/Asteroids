using System;
using _Project.Core.EventBus;
using _Project.Core.Input;

namespace _Project.Core.GameLifecycle
{
    public class PauseController : IDisposable
    {
        private readonly IPauseInputService _pauseInputService;
        private readonly IGameStateService _gameStateService;


        public PauseController(IPauseInputService pauseInputService, IGameStateService gameStateService)
        {
            _pauseInputService = pauseInputService;
            _gameStateService = gameStateService;
            _pauseInputService.OnPause += OnPause;
        }

        private void OnPause()
        {
            if (_gameStateService.CurrentState is GameState.Running)
            {
                _gameStateService.SetState(GameState.Paused);
            }
            else if (_gameStateService.CurrentState is GameState.Paused)
            {
                _gameStateService.SetState(GameState.Resume);
                _gameStateService.SetState(GameState.Running);
            }
        }

        public void Dispose()
        {
            _pauseInputService.OnPause -= OnPause;
        }
    }
}