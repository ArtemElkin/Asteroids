using System.Collections.Generic;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Core.Render.VFX;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Config;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Stun;
using _Project.Features.Spaceship.Weapon;
using _Project.Features.Spaceship.Weapon.LaserWeapon;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon;
using _Project.Infrastructure.Effects;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : AbstractFacadeFactory<SpaceshipSpawnData, SpaceshipFacade, SpaceshipView>
    {
        public SpaceshipFactory(IInstantiator instantiator, SpaceshipView prefab, Transform parentTransform) :
            base(instantiator, prefab, parentTransform) { }

        public override SpaceshipFacade Create(SpaceshipSpawnData data)
        {
            CreateView(data.initialMovementData.initialPosition,
                out var view, 
                out var drawable);
            CreatePhysicsComponents(data.initialMovementData, data.config.movementConfig, view,
                out var movementModel, 
                out var movable, 
                out var rotatable,
                out var collidable,
                out var boundsChecker);
            CreateWeapons(view, movementModel, data.config, 
                out var weapons);
            CreateHealth(data.config, out var healthController, 
                out var deathHandler);
            CreateClones(data.hasClones, movementModel, boundsChecker, drawable, 
                out var screenWrapCloneSet);
            CreateStunController(view, movementModel, data.config, collidable, screenWrapCloneSet, 
                out var stunController);

            CreateSpaceshipFacade(movementModel, movable, rotatable, boundsChecker, drawable, collidable,
                healthController, deathHandler, stunController, weapons, screenWrapCloneSet,
                out var facade);
            
            return facade;
        }

        private void CreateView(Vector2 initialPosition, out SpaceshipView view, out IDrawable drawable)
        {
            view = _pool.Get();
            drawable = view;
            drawable.Setup(initialPosition, 0);
        }

        private void CreatePhysicsComponents(
            InitialMovementData initialMovementData,
            SpaceshipMovementConfig movementConfig,
            SpaceshipView view,
            out MovementModel movementModel, 
            out IMovable movable,
            out IRotatable rotatable,
            out ICollidable collidable,
            out BoundsChecker boundsChecker
            )
        {
            movementModel = CreateComponent<MovementModel>(initialMovementData);
            collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel, true);
            
            movable = CreateComponent<SpaceshipMovementController>(movementModel, movementConfig);
            rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            boundsChecker = CreateComponent<BoundsChecker>(movementModel);
        }

        private void CreateWeapons(
            SpaceshipView view, 
            MovementModel movementModel, 
            SpaceshipConfig config, 
            out Dictionary<WeaponType, IWeapon> weapons)
        {
            weapons = new Dictionary<WeaponType, IWeapon>();
            
            MuzzleView muzzleView = view.GetMuzzleView();
            IHasPosition muzzlePosition = muzzleView;
            
            ProjectileWeapon projectileWeapon = CreateComponent<ProjectileWeapon>(muzzlePosition, movementModel, config.projectileWeaponConfig);
            weapons.Add(WeaponType.ProjectileWeapon, projectileWeapon);
            
            LaserWeapon laserWeapon =
                CreateComponent<LaserWeapon>(config.laserWeaponConfig, movementModel, muzzlePosition);
            weapons.Add(WeaponType.LaserWeapon, laserWeapon);
        }

        private void CreateStunController(
            SpaceshipView view, 
            MovementModel movementModel, 
            SpaceshipConfig config, 
            ICollidable collidable, 
            IScreenWrapCloneSet screenWrapCloneSet,
            out StunController stunController)
        {
            IEffect originStunEffect = view.GetComponentInChildren<IEffect>();
            IEffect syncedStunEffect = CreateComponent<SyncedSpaceshipStunEffect>(
                originStunEffect, 
                screenWrapCloneSet);
            
            stunController = CreateComponent<StunController>(
                config.stunDuration,
                movementModel, 
                collidable, 
                syncedStunEffect);
        }

        private void CreateClones(
            bool hasClones, 
            MovementModel movementModel, 
            BoundsChecker boundsChecker, 
            IDrawable drawable,
            out IScreenWrapCloneSet screenWrapCloneSet)
        {
            screenWrapCloneSet = hasClones
                ? CreateComponent<ScreenWrapCloneSet<SpaceshipFacade>>(
                    movementModel,
                    boundsChecker,
                    drawable)
                : new NullScreenWrapCloneSet();
            
            screenWrapCloneSet.CreateClones();
        }

        private void CreateHealth(
            SpaceshipConfig config, 
            out HealthController healthController, 
            out SpaceshipDeathHandler deathHandler)
        {
            var healthModel = CreateComponent<HealthModel>(config.maxHp); 
            
            healthController = CreateComponent<HealthController>(healthModel);
            deathHandler = CreateComponent<SpaceshipDeathHandler>();
        }

        private void CreateSpaceshipFacade(
            MovementModel movementModel,
            IMovable movable,
            IRotatable rotatable,
            BoundsChecker boundsChecker,
            IDrawable drawable,
            ICollidable collidable,
            HealthController healthController,
            SpaceshipDeathHandler deathHandler,
            StunController stunController,
            Dictionary<WeaponType, IWeapon> weapons,
            IScreenWrapCloneSet screenWrapCloneSet,
            out SpaceshipFacade facade)
        {
            facade = CreateComponent<SpaceshipFacade>(
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
        }
    }
}