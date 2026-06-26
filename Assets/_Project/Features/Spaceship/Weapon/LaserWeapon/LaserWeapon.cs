using System;
using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.LaserWeapon.Config;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using UnityEngine;

namespace _Project.Features.Spaceship.Weapon.LaserWeapon
{
    public class LaserWeapon : BaseWeapon<LaserWeaponConfig>, IReadOnlyLaserWeaponState
    {
        private int _availableBeamCount;
        private float _rechargeTimeLeft;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPosition _muzzlePosition;
        private readonly IFactory<LaserBeamSpawnData, LaserBeamFacade> _laserBeamFactory;
        public event Action<int> AvailableBeamCountChanged;
        public event Action<float> RechargeTimeLeftChanged;

        public int AvailableBeamCount
        {
            get => _availableBeamCount;
            private set
            {
                _availableBeamCount = value;
                AvailableBeamCountChanged?.Invoke(value);
            }
        }

        public float RechargeTimeLeft
        {
            get => _rechargeTimeLeft;
            private set
            {
                _rechargeTimeLeft = value;
                RechargeTimeLeftChanged?.Invoke(value);
            }
        }
        
        public LaserWeapon(
            LaserWeaponConfig config,
            IFireInputService fireInputService,
            IGameStateService gameStateService,
            MovementModel spaceshipMovementModel,
            ITimeService timeService,
            IReadOnlyPosition muzzlePosition,
            IFactory<LaserBeamSpawnData, LaserBeamFacade> laserBeamFactory) 
            : base(config, fireInputService, spaceshipMovementModel, gameStateService, timeService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePosition = muzzlePosition;
            _laserBeamFactory = laserBeamFactory;
            AvailableBeamCount = _config.maxBeamCount;
            RechargeTimeLeft = _config.oneBeamRechargeTime;
            _timeService.OnTick += OnTick;
        }

        private void OnTick(float deltaTime)
        {
            if (AvailableBeamCount == _config.maxBeamCount) return;
            
            if (RechargeTimeLeft > 0)
            {
                RechargeTimeLeft -= deltaTime;
                if (RechargeTimeLeft < 0) RechargeTimeLeft = 0;
            }
            else
            {
                AvailableBeamCount++;
                RechargeTimeLeft = AvailableBeamCount == _config.maxBeamCount ? 0 : _config.oneBeamRechargeTime;
            }
        }

        protected override void Shoot()
        {
            var spawnData = new LaserBeamSpawnData(
                _muzzlePosition.Position, 
                _spaceshipMovementModel.RotationAngle,
                _config.aliveTime);
            _laserBeamFactory.Create(spawnData);
            
            bool wasFull = AvailableBeamCount == _config.maxBeamCount;
            AvailableBeamCount--;
            if (wasFull)
            {
                RechargeTimeLeft = _config.oneBeamRechargeTime;
            }
        }

        protected override bool OptionalConditionToAllowFire => AvailableBeamCount > 0;

        public override void Dispose()
        {
            _timeService.OnTick -= OnTick;
            base.Dispose();
        }
    }
}