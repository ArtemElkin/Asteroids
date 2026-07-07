using System;
using _Project.Core.Audio;
using _Project.Core.GameLifecycle;
using UnityEngine;

namespace _Project.Infrastructure.Audio
{
    public class AudioPauseController : IDisposable
    {
        private readonly IAudioService<AudioClip> _audioService;
        private readonly IGameStateService _gameStateService;


        public AudioPauseController(IAudioService<AudioClip> audioService, IGameStateService gameStateService)
        {
            _audioService = audioService;
            _gameStateService = gameStateService;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Initialize:
                    _audioService.StopAllSounds();
                    break;
                case GameState.Paused:
                    _audioService.PauseSound();
                    break;
                case GameState.Resume:
                    _audioService.ResumeSound();
                    break;
            }
        }


        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}