using System;
using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Core.Player;

namespace _Project.Infrastructure.GameLifecycle
{
    public class RestartController : IDisposable
    {
        private readonly IGameStateService _gameStateService;
        private readonly List<IWorldResettable> _resettables;
        private readonly PlayerModel _playerModel;


        public RestartController(IGameStateService gameStateService, List<IWorldResettable> resettables, PlayerModel playerModel)
        {
            _gameStateService = gameStateService;
            _resettables = resettables;
            _playerModel = playerModel;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState is GameState.Restart)
            {
                foreach (var resettable in _resettables)
                {
                    resettable.Reset();
                }

                _playerModel.CurrentScore = 0;
                _gameStateService.SetState(GameState.Initialize);
                _gameStateService.SetState(GameState.Running);
            }
        }


        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}