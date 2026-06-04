using System;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Signals;
using _Project.Features.Projectile;
using UnityEngine;

namespace _Project.Features.Spaceship.Weapon
{
    public class Weapon : IDisposable
    {
        private readonly WeaponConfig _config;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly ISignalBus _signalBus;
        private readonly IFireInputService _fireInputService;
        private readonly SpawnTimer _projectileSpawnTimer;
        private readonly IScreenService _screenService;


        public Weapon(
            IReadOnlyPositionable muzzlePositionable,
            ISignalBus signalBus,
            IFireInputService fireInputService,
            SpawnTimer projectileSpawnTimer,
            IScreenService screenService)
        {
            _muzzlePositionable = muzzlePositionable;
            _signalBus = signalBus;
            _fireInputService = fireInputService;
            _projectileSpawnTimer = projectileSpawnTimer;
            _screenService = screenService;
            _projectileSpawnTimer.OnSpawnRequested += OnSpawnRequested;
            _projectileSpawnTimer.Setup(1f);
            _projectileSpawnTimer.Start();
            Debug.Log("Weapon setup");
        }

        private void OnSpawnRequested()
        {
            Debug.Log("Spawn projectile Requested");
            if (_fireInputService.FireState == true)
            {
                Debug.Log("Firing projectile");
                var initialPosition = _muzzlePositionable.Position;
                var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
                var initialDirection = (targetPosition - initialPosition).normalized;
                Debug.Log($"initial position: {initialPosition}\ntarget position:{targetPosition}\ndirection:{initialDirection}");
                var initialSpeed = 5f;
                var initialVelocity = initialDirection * initialSpeed;
                var initialMovementData = new InitialMovementData(initialPosition, initialVelocity);
                _signalBus.Fire(new SpawnRequestedSignal<ProjectileFacade>(initialMovementData));
            }
        }


        public void Dispose()
        {
            _projectileSpawnTimer.OnSpawnRequested -= OnSpawnRequested;
        }
    }
}