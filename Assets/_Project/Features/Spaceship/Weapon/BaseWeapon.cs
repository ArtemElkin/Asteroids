using System;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;

namespace _Project.Features.Spaceship.Weapon
{
    public abstract class BaseWeapon<TWeaponConfig> : IWeapon where TWeaponConfig : WeaponConfig
    {
        private float _timeFromLastShot;
        protected readonly IFireInputService _fireInputService;
        protected readonly TWeaponConfig _config;
        private readonly float _cooldown;
        private readonly IStunable _stundable;
        private readonly ITimeService _timeService;
        protected virtual bool OptionalConditionToAllowFire => true;
        
        
        protected BaseWeapon(
            TWeaponConfig config,
            IFireInputService fireInputService,
            IStunable stundable,
            ITimeService timeService)
        {
            _config = config;
            _fireInputService = fireInputService;
            _stundable = stundable;
            _timeService = timeService;
            _timeService.OnTick += OnTick;
            _cooldown = 1 / _config.shootsPerSecond;
            _timeFromLastShot = _cooldown;
        }

        private void OnTick(float deltaTime)
        {
            bool fireAllowed = _timeFromLastShot >= _cooldown;
            
            if (!fireAllowed) _timeFromLastShot += deltaTime;

            if (_fireInputService.FireState(_config.mouseButtonId) && fireAllowed && !_stundable.IsStunned && OptionalConditionToAllowFire)
            {
                Shoot();
                _timeFromLastShot = 0;
            }
        }


        protected abstract void Shoot();
        
        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}