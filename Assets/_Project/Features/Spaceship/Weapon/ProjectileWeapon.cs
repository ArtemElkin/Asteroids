using _Project.Core.Factories;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.Config;
using _Project.Features.Spaceship.Weapon.Projectile;

namespace _Project.Features.Spaceship.Weapon
{
    public class ProjectileWeapon : BaseWeapon
    {
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPositionable _muzzlePositionable;
        private readonly IFactory<ProjectileSpawnData, ProjectileFacade> _projectileFactory;
        private readonly IScreenService _screenService;

        
        public ProjectileWeapon(
            ProjectileWeaponConfig config,
            IFireInputService fireInputService,
            ITimeService timeService,
            MovementModel spaceshipMovementModel,
            IReadOnlyPositionable muzzlePositionable,
            IFactory<ProjectileSpawnData, ProjectileFacade> projectileFactory,
            IScreenService screenService) : base(config, fireInputService, timeService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePositionable = muzzlePositionable;
            _projectileFactory = projectileFactory;
            _screenService = screenService;
        }

        public override void Shoot()
        {
            var initialPosition = _muzzlePositionable.Position;
            var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var initialDirection = (targetPosition - initialPosition).normalized;
            var initialSpeed = 30f;
            var initialVelocity = _spaceshipMovementModel.Velocity + initialDirection * initialSpeed;
            var spawnData = new ProjectileSpawnData(new InitialMovementData(1f, initialPosition, initialVelocity));
            _projectileFactory.Create(spawnData);
        }
    }
}