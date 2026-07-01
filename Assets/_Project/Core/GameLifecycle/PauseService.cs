using System;
using _Project.Core.Input;

namespace _Project.Core.GameLifecycle
{
    public class PauseService : IPauseService, IDisposable
    {
        private readonly IPauseInputService _pauseInputService;
        private readonly IGameStateService _gameStateService;


        public PauseService(IPauseInputService pauseInputService, IGameStateService gameStateService)
        {
            _pauseInputService = pauseInputService;
            _gameStateService = gameStateService;
            _pauseInputService.OnPause += OnPause;
        }

        public void Pause()
        {
            _gameStateService.SetState(GameState.Paused);
        }

        public void Resume()
        {
            _gameStateService.SetState(GameState.Resume);
            _gameStateService.SetState(GameState.Running);
        }

        private void OnPause()
        {
            if (_gameStateService.CurrentState is GameState.Running)
            {
                Pause();
            }
            else if (_gameStateService.CurrentState is GameState.Paused)
            {
                Resume();
            }
        }

        public void Dispose()
        {
            _pauseInputService.OnPause -= OnPause;
        }
    }
}