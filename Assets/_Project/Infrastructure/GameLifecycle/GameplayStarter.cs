using _Project.Core.GameLifecycle;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.GameLifecycle
{
    public class GameplayStarter : MonoBehaviour
    {
        private IGameStateService _gameStateService;


        private void Start()
        {
            _gameStateService.SetState(GameState.Initialize);
            _gameStateService.SetState(GameState.Running);
        }

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
    }
}