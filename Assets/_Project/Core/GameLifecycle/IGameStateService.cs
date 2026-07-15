using System;

namespace _Project.Core.GameLifecycle
{
    public interface IGameStateService
    {
        GameState CurrentState { get; }
        public void SetState(GameState newState);
        event Action<GameState, TransitionType> OnGameStateChanged;
    }
}