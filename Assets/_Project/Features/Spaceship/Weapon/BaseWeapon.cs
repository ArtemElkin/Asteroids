using System;
using _Project.Core.Input;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;

namespace _Project.Features.Spaceship.Weapon
{
    public abstract class BaseWeapon : IShootable, IDisposable
    {
        private float _timeFromLastShot;
        protected readonly IFireInputService _fireInputService;
        private readonly WeaponConfig _config;
        private readonly ITimeService _timeService;
        
        
        protected BaseWeapon(
            WeaponConfig config,
            IFireInputService fireInputService,
            ITimeService timeService)
        {
            _config = config;
            _fireInputService = fireInputService;
            _timeService = timeService;
            _timeService.OnTick += OnTick;
        }

        private void OnTick(float deltaTime)
        {
            var cooldown = 1 / _config.shootsPerSecond;
            bool fireAllowed = _timeFromLastShot >= cooldown;
            
            if (!fireAllowed) _timeFromLastShot += deltaTime;

            if (_fireInputService.FireState(_config.mouseButtonId) && fireAllowed)
            {
                Shoot();
                _timeFromLastShot = 0;
            }
        }

        public abstract void Shoot();
        
        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}