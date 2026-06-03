using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Health;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : AbstractFactory<SpaceshipSpawnData, SpaceshipFacade>
    {
        public SpaceshipFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override SpaceshipFacade Create(SpaceshipSpawnData data)
        {
            MovableView view = _viewPool.Get();
            InitialMovementData initialMovementData = data.InitialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IMovable movable = CreateComponent<SpaceshipMovementController>(movementModel, data.movementConfig);
            IRotatable rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            IBouncable bouncable = CreateComponent<BounceController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel, movable);
            HealthModel healthModel = CreateComponent<HealthModel>(data.initialHp);
            HealthController healthController = CreateComponent<HealthController>(healthModel);
            StunController stunController = CreateComponent<StunController>(movementModel, collidable);
            SpaceshipFacade facade = CreateComponent<SpaceshipFacade>(
                movementModel,
                movable,
                rotatable,
                bouncable,
                boundsChecker,
                drawable,
                healthController,
                collidable,
                stunController);
            return facade;
        }
    }
}