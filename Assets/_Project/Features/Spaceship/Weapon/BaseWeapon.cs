using System;
using _Project.Core.GameLifecycle;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;

namespace _Project.Features.Spaceship.Weapon
{
    public abstract class BaseWeapon<TWeaponConfig> : IWeapon where TWeaponConfig : WeaponConfig
    {
        protected readonly IFireInputService _fireInputService;
        protected readonly TWeaponConfig _config;
        protected readonly ITimeService _timeService;
        private float _timeFromLastShot;
        private readonly float _cooldown;
        private readonly IMutableStun _stundable;
        private readonly IGameStateService _gameStateService;
        protected virtual bool OptionalConditionToAllowFire => true;


        protected BaseWeapon(
            TWeaponConfig config,
            IFireInputService fireInputService,
            IMutableStun stundable,
            IGameStateService gameStateService,
            ITimeService timeService)
        {
            _config = config;
            _fireInputService = fireInputService;
            _stundable = stundable;
            _gameStateService = gameStateService;
            _timeService = timeService;
            _timeService.OnTick += OnTick;
            _cooldown = 1 / _config.shootsPerSecond;
            _timeFromLastShot = _cooldown;
        }

        private void OnTick(float deltaTime)
        {
            if (_gameStateService.CurrentState != GameState.Running) return;
            
            bool fireAllowed = _timeFromLastShot >= _cooldown;
            
            if (!fireAllowed) _timeFromLastShot += deltaTime;

            if (_fireInputService.FireState(_config.mouseButtonId) && fireAllowed && !_stundable.IsStunned && OptionalConditionToAllowFire)
            {
                Shoot();
                _timeFromLastShot = 0;
            }
        }


        protected abstract void Shoot();
        
        public virtual void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}