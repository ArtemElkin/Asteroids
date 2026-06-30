using _Project.Core.GameLifecycle;
using _Project.Core.Render.VFX;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Render
{
    public class EffectPauseController : MonoBehaviour
    {
        private IGameStateService _gameStateService;
        private IEffect _effect;


        private void Awake()
        {
            _effect = GetComponent<IEffect>();
        }
        
        private void OnEnable()
        {
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Paused:
                    _effect.Pause();
                    break;
                case GameState.Running:
                    if (_effect.IsPaused) _effect.Play();
                    break;
                case GameState.Restart:
                    _effect.Stop();
                    break;
            }
        }
        
        private void OnDisable()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}