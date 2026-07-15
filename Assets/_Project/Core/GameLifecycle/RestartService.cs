using _Project.Core.Player;

namespace _Project.Core.GameLifecycle
{
    public class RestartService : IRestartService
    {
        private readonly IGameStateService _gameStateService;
        private readonly IWorldResetService _resetService;
        private readonly PlayerModel _playerModel;


        public RestartService(IGameStateService gameStateService, IWorldResetService resetService, PlayerModel playerModel)
        {
            _gameStateService = gameStateService;
            _resetService =  resetService;
            _playerModel = playerModel;
        }

        public void Restart()
        {
            if (_gameStateService.CurrentState is not GameState.GameOver) return;
            
            _resetService.ResetWorld();
            _playerModel.CurrentScore = 0;
            _gameStateService.SetState(GameState.Running);
        }
    }
}