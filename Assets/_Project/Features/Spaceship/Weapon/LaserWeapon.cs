using _Project.Core.Factories;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;
using _Project.Features.Spaceship.Weapon.LaserBeam;
using UnityEngine;

namespace _Project.Features.Spaceship.Weapon
{
    public class LaserWeapon : BaseWeapon<LaserWeaponConfig>
    {
        private int _availableBeamCount;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly IFactory<LaserBeamSpawnData, LaserBeamFacade> _laserBeamFactory;
        private readonly Timer _timer;
        
        public LaserWeapon(
            LaserWeaponConfig config,
            IFireInputService fireInputService,
            MovementModel spaceshipMovementModel,
            ITimeService timeService,
            Timer timer,
            IReadOnlyPositionable muzzlePositionable,
            IFactory<LaserBeamSpawnData, LaserBeamFacade> laserBeamFactory) 
            : base(config, fireInputService, spaceshipMovementModel, timeService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _laserBeamFactory = laserBeamFactory;
            _availableBeamCount = _config.maxBeamCount;
            _timer = timer;
            _timer.Start(config.oneBeamRechargeTime, true);
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed()
        {
            _availableBeamCount++;
            if (_availableBeamCount > _config.maxBeamCount)
            {
                _availableBeamCount = _config.maxBeamCount;
            }
        }

        protected override void Shoot()
        {
            var spawnData = new LaserBeamSpawnData(
                _muzzlePositionable.Position, 
                _spaceshipMovementModel.RotationAngle,
                _config.aliveTime);
            _laserBeamFactory.Create(spawnData);
            
            _availableBeamCount--;
        }

        protected override bool OptionalConditionToAllowFire => _availableBeamCount > 0;
    }
}