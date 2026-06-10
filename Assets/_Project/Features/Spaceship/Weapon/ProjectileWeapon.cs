using System;
using _Project.Core.EventBus;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Common.Event;
using _Project.Features.Spaceship.Weapon.Config;
using _Project.Features.Spaceship.Weapon.Projectile;

namespace _Project.Features.Spaceship.Weapon
{
    public class ProjectileWeapon : IShootable, IDisposable
    {
        private float _timeFromLastShot;
        private readonly WeaponConfig _config;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly IEventBus _eventBus;
        private readonly IFireInputService _fireInputService;
        private readonly ITimeService _timeService;
        private readonly IScreenService _screenService;


        public ProjectileWeapon(
            WeaponConfig config,
            MovementModel spaceshipMovementModel,
            IReadOnlyPositionable muzzlePositionable,
            IEventBus eventBus,
            IFireInputService fireInputService,
            ITimeService timeService,
            IScreenService screenService)
        {
            _config = config;
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _eventBus = eventBus;
            _fireInputService = fireInputService;
            _timeService = timeService;
            _screenService = screenService;
            _timeService.OnTick += OnTick;
        }

        public void Shoot()
        {
            var initialPosition = _muzzlePositionable.Position;
            var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var initialDirection = (targetPosition - initialPosition).normalized;
            var initialSpeed = 30f;
            var initialVelocity = _spaceshipMovementModel.Velocity + initialDirection * initialSpeed;
            var initialMovementData = new InitialMovementData(1f, initialPosition, initialVelocity);
            _eventBus.Publish(new SpawnRequestedEvent<ProjectileFacade>(initialMovementData));
        }

        private void OnTick(float deltaTime)
        {
            var cooldown = 1 / _config.projectilesPerSecond;
            bool fireAllowed = _timeFromLastShot >= cooldown;
            
            if (!fireAllowed) _timeFromLastShot += deltaTime;

            if (_fireInputService.FireState && fireAllowed)
            {
                Shoot();
                _timeFromLastShot = 0;
            }
        }


        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}