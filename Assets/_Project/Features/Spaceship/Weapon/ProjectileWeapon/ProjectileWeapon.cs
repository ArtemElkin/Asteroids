using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Input;
using _Project.Core.Physics;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Config;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Projectile;

namespace _Project.Features.Spaceship.Weapon.ProjectileWeapon
{
    public class ProjectileWeapon : BaseWeapon<ProjectileWeaponConfig>
    {
        private readonly MovementModel _spaceshipMovementModel;
        private readonly IReadOnlyPosition _muzzlePosition;
        private readonly IFactory<ProjectileSpawnData, ProjectileFacade> _projectileFactory;
        private readonly IScreenService _screenService;

        
        public ProjectileWeapon(
            ProjectileWeaponConfig config,
            IFireInputService fireInputService,
            IGameStateService gameStateService,
            ITimeService timeService,
            MovementModel spaceshipMovementModel,
            IReadOnlyPosition muzzlePosition,
            IFactory<ProjectileSpawnData, ProjectileFacade> projectileFactory,
            IScreenService screenService) : base(config, fireInputService, spaceshipMovementModel, gameStateService, timeService)
        {
            _spaceshipMovementModel = spaceshipMovementModel;
            _muzzlePosition = muzzlePosition;
            _projectileFactory = projectileFactory;
            _screenService = screenService;
        }

        protected override void Shoot()
        {
            var initialPosition = _muzzlePosition.Position;
            var targetPosition = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var initialDirection = (targetPosition - initialPosition).normalized;
            var initialSpeed = 30f;
            var initialVelocity = _spaceshipMovementModel.Velocity + initialDirection * initialSpeed;
            var spawnData = new ProjectileSpawnData(new InitialMovementData(1f, initialPosition, initialVelocity), _config.aliveTime);
            _projectileFactory.Create(spawnData);
        }
    }
}