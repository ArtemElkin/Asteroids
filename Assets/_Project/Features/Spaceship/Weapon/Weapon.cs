using System;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Signals;
using _Project.Features.Projectile;

namespace _Project.Features.Spaceship.Weapon
{
    public class Weapon : IDisposable
    {
        private readonly WeaponConfig _config;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly ISignalBus _signalBus;
        private readonly IFireInputService _fireInputService;
        private readonly Timer _timer;
        private readonly SpawnTimer _projectileSpawnTimer;
        private readonly IScreenService _screenService;


        public Weapon(
            MovementModel spaceshipMovementModel,
            IReadOnlyPositionable muzzlePositionable,
            ISignalBus signalBus,
            IFireInputService fireInputService,
            Timer timer,
            SpawnTimer projectileSpawnTimer,
            IScreenService screenService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _signalBus = signalBus;
            _fireInputService = fireInputService;
            _timer = timer;
            _projectileSpawnTimer = projectileSpawnTimer;
            _screenService = screenService;
            _projectileSpawnTimer.OnSpawnRequested += OnSpawnRequested;
            _projectileSpawnTimer.Setup(0.3f);
            _projectileSpawnTimer.Start();
        }

        private void OnSpawnRequested()
        {
            if (_fireInputService.FireState == true)
            {
                var initialPosition = _muzzlePositionable.Position;
                var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
                var initialDirection = (targetPosition - initialPosition).normalized;
                var initialSpeed = 30f;
                var initialVelocity = _spaceshipMovementModel.Velocity + initialDirection * initialSpeed;
                var initialMovementData = new InitialMovementData(1f, initialPosition, initialVelocity);
                _signalBus.Fire(new SpawnRequestedSignal<ProjectileFacade>(initialMovementData));
            }
        }


        public void Dispose()
        {
            _projectileSpawnTimer.OnSpawnRequested -= OnSpawnRequested;
        }
    }
}