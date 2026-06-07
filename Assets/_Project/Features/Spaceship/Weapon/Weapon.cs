using System;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;
using _Project.Features.Projectile;

namespace _Project.Features.Spaceship.Weapon
{
    public class Weapon : IDisposable
    {
        private float _cooldown;
        private float _timeFromLastShot;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly ISignalBus _signalBus;
        private readonly IFireInputService _fireInputService;
        private readonly ITimeService _timeService;
        private readonly IScreenService _screenService;


        public Weapon(
            WeaponConfig weaponConfig,
            MovementModel spaceshipMovementModel,
            IReadOnlyPositionable muzzlePositionable,
            ISignalBus signalBus,
            IFireInputService fireInputService,
            ITimeService timeService,
            IScreenService screenService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _signalBus = signalBus;
            _fireInputService = fireInputService;
            _timeService = timeService;
            _screenService = screenService;
            
            _timeService.OnTick += OnTick;

            _cooldown = 1 / weaponConfig.projectilesPerSecond;
        }

        private void Shoot()
        {
            var initialPosition = _muzzlePositionable.Position;
            var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var initialDirection = (targetPosition - initialPosition).normalized;
            var initialSpeed = 30f;
            var initialVelocity = _spaceshipMovementModel.Velocity + initialDirection * initialSpeed;
            var initialMovementData = new InitialMovementData(1f, initialPosition, initialVelocity);
            _signalBus.Fire(new SpawnRequestedSignal<ProjectileFacade>(initialMovementData));
        }

        private void OnTick(float deltaTime)
        {
            bool fireAllowed = _timeFromLastShot >= _cooldown;
            
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