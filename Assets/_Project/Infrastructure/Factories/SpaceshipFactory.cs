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
            var viewParts = CreateView(data.initialMovementData.initialPosition);
            var physicsParts = CreatePhysicsComponents(data.initialMovementData, 
                data.config.movementConfig, viewParts.View);
            var weapons = CreateWeapons(viewParts.View, 
                physicsParts.Model, data.config);
            var healthParts = CreateHealth(data.config);
            var screenWrapCloneSet = CreateClones(data.hasClones, physicsParts.Model, 
                physicsParts.BoundsChecker, viewParts.Drawable);
            var stunController = CreateStunController(viewParts.View, physicsParts.Model, data.config, 
                physicsParts.Collidable, screenWrapCloneSet);

            var facade = CreateSpaceshipFacade(viewParts, physicsParts, 
                healthParts, stunController, weapons, screenWrapCloneSet);
            
            return facade;
        }
        
        private struct ViewParts
        {
            public SpaceshipView View { get; }
            public IDrawable Drawable { get; }

            public ViewParts(SpaceshipView view, IDrawable drawable)
            {
                View = view;
                Drawable = drawable;
            }
        }

        private ViewParts CreateView(Vector2 initialPosition)
        {
            var view = _pool.Get();
            var drawable = view;
            drawable.Setup(initialPosition, 0);
            
            return new ViewParts(view, drawable);
        }

        private struct PhysicsParts
        {
            public MovementModel Model { get; }
            public IMovable Movable { get; }
            public IRotatable Rotatable { get; }
            public ICollidable Collidable { get; }
            public BoundsChecker BoundsChecker { get; }

            public PhysicsParts(MovementModel model, IMovable movable, IRotatable rotatable, 
                ICollidable collidable, BoundsChecker boundsChecker)
            {
                Model = model;
                Movable = movable;
                Rotatable = rotatable;
                Collidable = collidable;
                BoundsChecker = boundsChecker;
            }
        }

        private PhysicsParts CreatePhysicsComponents(
            InitialMovementData initialMovementData,
            SpaceshipMovementConfig movementConfig,
            SpaceshipView view)
        {
            var movementModel = CreateComponent<MovementModel>(initialMovementData);
            var collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel, true);
            
            var movable = CreateComponent<SpaceshipMovementController>(movementModel, movementConfig);
            var rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            var boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            
            return new PhysicsParts(movementModel,  movable, rotatable, collidable, boundsChecker);
        }

        private Dictionary<WeaponType, IWeapon> CreateWeapons(
            SpaceshipView view, 
            MovementModel movementModel, 
            SpaceshipConfig config)
        {
            var weapons = new Dictionary<WeaponType, IWeapon>();
            
            MuzzleView muzzleView = view.GetMuzzleView();
            IHasPosition muzzlePosition = muzzleView;
            
            ProjectileWeapon projectileWeapon = CreateComponent<ProjectileWeapon>(muzzlePosition, movementModel, config.projectileWeaponConfig);
            weapons.Add(WeaponType.ProjectileWeapon, projectileWeapon);
            
            LaserWeapon laserWeapon =
                CreateComponent<LaserWeapon>(config.laserWeaponConfig, movementModel, muzzlePosition);
            weapons.Add(WeaponType.LaserWeapon, laserWeapon);
            
            return weapons;
        }

        private StunController CreateStunController(
            SpaceshipView view, 
            MovementModel movementModel, 
            SpaceshipConfig config, 
            ICollidable collidable, 
            IScreenWrapCloneSet screenWrapCloneSet)
        {
            IEffect originStunEffect = view.GetComponentInChildren<IEffect>();
            IEffect syncedStunEffect = CreateComponent<SyncedSpaceshipStunEffect>(
                originStunEffect, 
                screenWrapCloneSet);
            
            return CreateComponent<StunController>(
                config.stunDuration,
                movementModel, 
                collidable, 
                syncedStunEffect);
        }

        private IScreenWrapCloneSet CreateClones(
            bool hasClones, 
            MovementModel movementModel, 
            BoundsChecker boundsChecker, 
            IDrawable drawable)
        {
            IScreenWrapCloneSet screenWrapCloneSet = hasClones
                ? CreateComponent<ScreenWrapCloneSet<SpaceshipFacade>>(
                    movementModel,
                    boundsChecker,
                    drawable)
                : new NullScreenWrapCloneSet();
            
            screenWrapCloneSet.CreateClones();
            
            return screenWrapCloneSet;
        }

        private struct HealthParts
        {
            public HealthController HealthController { get; }
            public SpaceshipDeathHandler DeathHandler { get; }

            public HealthParts(HealthController healthController, SpaceshipDeathHandler deathHandler)
            {
                HealthController = healthController;
                DeathHandler = deathHandler;
            }
        }

        private HealthParts CreateHealth(SpaceshipConfig config)
        {
            var healthModel = CreateComponent<HealthModel>(config.healthConfig.maxHealth); 
            
            var healthController = CreateComponent<HealthController>(healthModel);
            var deathHandler = CreateComponent<SpaceshipDeathHandler>();
            
            return new HealthParts(healthController, deathHandler);
        }

        private SpaceshipFacade CreateSpaceshipFacade(
            ViewParts viewParts,
            PhysicsParts physicsParts,
            HealthParts healthParts,
            StunController stunController,
            Dictionary<WeaponType, IWeapon> weapons,
            IScreenWrapCloneSet screenWrapCloneSet)
        {
            return CreateComponent<SpaceshipFacade>(
                physicsParts.Model,
                physicsParts.Movable,
                physicsParts.Rotatable,
                physicsParts.Collidable,
                physicsParts.BoundsChecker,
                viewParts.Drawable,
                healthParts.HealthController,
                healthParts.DeathHandler,
                stunController,
                weapons,
                screenWrapCloneSet);
        }
    }
}