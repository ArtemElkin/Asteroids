using System;

namespace _Project.Core.GameLifecycle
{
    public class GameStateService : IGameStateService
    {
        public GameState CurrentState { get; private set; } = GameState.None;
        public event Action<GameState, TransitionType> OnGameStateChanged;

        public void SetState(GameState newState)
        {
            if (CurrentState != newState)
            {
                TransitionType transitionType;
                if ((CurrentState is GameState.None or GameState.GameOver) && newState is GameState.Running) transitionType = TransitionType.OnStart;
                else if (CurrentState is GameState.Running && newState is GameState.Paused) transitionType = TransitionType.OnPause;
                else if (CurrentState is GameState.Paused && newState is GameState.Running) transitionType = TransitionType.OnResume;
                else if (CurrentState is GameState.Running && newState is GameState.GameOver)
                    transitionType = TransitionType.OnStop;
                else
                {
                    throw new InvalidOperationException($"Invalid transition: {CurrentState} -> {newState}");
                }
                CurrentState = newState;
                OnGameStateChanged?.Invoke(newState, transitionType);
            }
        }
    }
}