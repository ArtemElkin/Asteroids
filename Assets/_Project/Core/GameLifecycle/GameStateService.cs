using System;

namespace _Project.Core.GameLifecycle
{
    public class GameStateService : IGameStateService
    {
        public GameState CurrentState { get; private set; } = GameState.None;
        public event Action<GameState> OnGameStateChanged;

        public void SetState(GameState newState)
        {
            CurrentState = newState;
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }
}