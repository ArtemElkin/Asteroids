using _Project.Core.Factories;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;
using _Project.Features.Spaceship.Weapon.LaserBeam;

namespace _Project.Features.Spaceship.Weapon
{
    public class LaserWeapon : BaseWeapon
    {
        private readonly float _laserBeamAliveTime;
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly IFactory<LaserBeamSpawnData, LaserBeamFacade> _laserBeamFactory;
        
        public LaserWeapon(
            LaserWeaponConfig config,
            IFireInputService fireInputService,
            ITimeService timeService,
            MovementModel spaceshipMovementModel,
            IReadOnlyPositionable muzzlePositionable,
            IFactory<LaserBeamSpawnData, LaserBeamFacade> laserBeamFactory) 
            : base(config, fireInputService, timeService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _laserBeamFactory = laserBeamFactory;
            _laserBeamAliveTime = config.aliveTime;
        }


        public override void Shoot()
        {
            var spawnData = new LaserBeamSpawnData(
                _muzzlePositionable.Position, 
                _spaceshipMovementModel.RotationAngle,
                _laserBeamAliveTime);
            _laserBeamFactory.Create(spawnData);
        }
    }
}