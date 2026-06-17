using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Effect;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Stun;
using _Project.Features.Spaceship.Weapon;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : AbstractFacadeFactory<SpaceshipSpawnData, SpaceshipFacade, SpaceshipView>
    {
        public SpaceshipFactory(IInstantiator instantiator, SpaceshipView prefab, Transform parentTransform) :
            base(instantiator, prefab, parentTransform) { }

        public override SpaceshipFacade Create(SpaceshipSpawnData data)
        {
            SpaceshipView view = _pool.Get();
            MuzzleView muzzleView = view.GetMuzzleView();
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel);
            IMovable movable = CreateComponent<SpaceshipMovementController>(movementModel, data.config.movementConfig);
            IRotatable rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            HealthModel healthModel = CreateComponent<HealthModel>(data.config.maxHp);
            HealthController healthController = CreateComponent<HealthController>(healthModel);
            
            IScreenWrapCloneSet screenWrapCloneSet = data.config.hasClones
                ? CreateComponent<ScreenWrapCloneSet<SpaceshipFacade>>(
                    movementModel,
                    boundsChecker,
                    drawable)
                : new NullScreenWrapCloneSet();
            IReadOnlyPositionable muzzlePositionable = muzzleView;

            IEffect originStunEffect = view.GetComponentInChildren<IEffect>();
            IEffect syncedStunEffect = CreateComponent<SyncedSpaceshipStunEffect>(originStunEffect, screenWrapCloneSet);
            StunController stunController = CreateComponent<StunController>(movementModel, collidable, syncedStunEffect);
            
            ProjectileWeapon projectileWeapon = CreateComponent<ProjectileWeapon>(muzzlePositionable, movementModel, data.config.projectileWeaponConfig);
            LaserWeapon laserWeapon =
                CreateComponent<LaserWeapon>(data.config.laserWeaponConfig, movementModel, muzzlePositionable);
            var weapons = new IWeapon[] { projectileWeapon, laserWeapon };
            
            SpaceshipFacade facade = CreateComponent<SpaceshipFacade>(
                movementModel,
                movable,
                rotatable,
                boundsChecker,
                drawable,
                healthController,
                collidable,
                stunController,
                weapons,
                screenWrapCloneSet);
            return facade;
        }
    }
}