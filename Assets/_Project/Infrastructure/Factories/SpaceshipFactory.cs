using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Weapon;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : AbstractFactory<SpaceshipSpawnData, SpaceshipFacade>
    {
        public SpaceshipFactory(IInstantiator instantiator, SpaceshipView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override SpaceshipFacade Create(SpaceshipSpawnData data)
        {
            SpaceshipView view = (SpaceshipView)_viewPool.Get();
            MuzzleView muzzleView = view.GetMuzzleView();
            InitialMovementData initialMovementData = data.InitialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IMovable movable = CreateComponent<SpaceshipMovementController>(movementModel, data.movementConfig);
            IRotatable rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            IBouncable bouncable = CreateComponent<BounceController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            HealthModel healthModel = CreateComponent<HealthModel>(data.initialHp);
            HealthController healthController = CreateComponent<HealthController>(healthModel);
            StunController stunController = CreateComponent<StunController>(movementModel, collidable);

            IReadOnlyPositionable muzzlePositionable = muzzleView;
            Weapon weapon = CreateComponent<Weapon>(muzzlePositionable, movementModel);
            
            SpaceshipFacade facade = CreateComponent<SpaceshipFacade>(
                movementModel,
                movable,
                rotatable,
                bouncable,
                boundsChecker,
                drawable,
                healthController,
                collidable,
                stunController,
                weapon);
            return facade;
        }
    }
}