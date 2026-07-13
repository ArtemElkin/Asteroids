using System.Collections.Generic;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Core.Render.VFX;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Stun;
using _Project.Features.Spaceship.Weapon;
using _Project.Features.Spaceship.Weapon.LaserWeapon;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon;
using _Project.Infrastructure.Effects;
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
            collidable.Setup(movementModel, true);
            IMovable movable = CreateComponent<SpaceshipMovementController>(movementModel, data.config.movementConfig);
            IRotatable rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            HealthModel healthModel = CreateComponent<HealthModel>(data.config.maxHp);
            HealthController healthController = CreateComponent<HealthController>(healthModel);
            SpaceshipDeathHandler deathHandler = CreateComponent<SpaceshipDeathHandler>();
            IScreenWrapCloneSet screenWrapCloneSet = data.hasClones
                ? CreateComponent<ScreenWrapCloneSet<SpaceshipFacade>>(
                    movementModel,
                    boundsChecker,
                    drawable)
                : new NullScreenWrapCloneSet();
            IHasPosition muzzlePosition = muzzleView;

            IEffect originStunEffect = view.GetComponentInChildren<IEffect>();
            IEffect syncedStunEffect = CreateComponent<SyncedSpaceshipStunEffect>(originStunEffect, screenWrapCloneSet);
            StunController stunController = CreateComponent<StunController>(
                data.config.stunDuration,
                movementModel, 
                collidable, 
                syncedStunEffect);

            var weapons = new Dictionary<WeaponType, IWeapon>();
            ProjectileWeapon projectileWeapon = CreateComponent<ProjectileWeapon>(muzzlePosition, movementModel, data.config.projectileWeaponConfig);
            weapons.Add(WeaponType.ProjectileWeapon, projectileWeapon);
            LaserWeapon laserWeapon =
                CreateComponent<LaserWeapon>(data.config.laserWeaponConfig, movementModel, muzzlePosition);
            weapons.Add(WeaponType.LaserWeapon, laserWeapon);
            
            SpaceshipFacade facade = CreateComponent<SpaceshipFacade>(
                movementModel,
                movable,
                rotatable,
                boundsChecker,
                drawable,
                healthController,
                deathHandler,
                collidable,
                stunController,
                weapons,
                screenWrapCloneSet);
            return facade;
        }
    }
}