using System;
using _Project.Core.GameLifecycle;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Render.VFX;
using _Project.Core.Services;
using _Project.Features.Spaceship.Config;

namespace _Project.Features.Spaceship.Stun
{
    public class StunController : IDisposable
    {
        private readonly float _stunDuration;
        private readonly IMutableStun _mutableStun;
        private readonly ICollidable _collidable;
        private readonly IGameStateService _gameStateService;
        private readonly Timer _timer;
        private readonly IEffect _stunEffect;


        public StunController(
            float stunDuration,
            IMutableStun mutableStun, 
            ICollidable collidable,
            IGameStateService gameStateService,
            Timer timer,
            IEffect stunEffect)
        {
            _stunDuration = stunDuration;
            _mutableStun = mutableStun;
            _collidable = collidable;
            _gameStateService = gameStateService;
            _timer = timer;
            _timer.Elapsed += Elapsed;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
            _stunEffect = stunEffect;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (!_mutableStun.IsStunned) return;
            
            switch (gameState)
            {
                case GameState.Paused:
                    _timer.Pause();
                    break;
                case GameState.Running:
                    _timer.Resume();
                    break;
            }
        }

        private void Elapsed()
        {
            EndStun();
        }

        private void EndStun()
        {
            _mutableStun?.SetStunned(false);
            _collidable?.ActivateCollision();
            _stunEffect?.Stop();
        }

        public void ApplyStun()
        {
            _timer.Start(_stunDuration);
            _mutableStun.SetStunned(true);
            _collidable.DeactivateCollision();
            _stunEffect.Play();
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
            _mutableStun.SetStunned(false);
            _stunEffect.Stop();
            _timer.Elapsed -= Elapsed;
            _timer.Dispose();
        }
    }
}