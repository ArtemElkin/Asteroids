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
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel);
            IMovable movable = CreateComponent<SpaceshipMovementController>(movementModel, data.config.movementConfig);
            IRotatable rotatable = CreateComponent<SpaceshipRotationController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            HealthModel healthModel = CreateComponent<HealthModel>(data.initialHp);
            HealthController healthController = CreateComponent<HealthController>(healthModel);
            StunController stunController = CreateComponent<StunController>(movementModel, collidable);

            IReadOnlyPositionable muzzlePositionable = muzzleView;
            Weapon weapon = CreateComponent<Weapon>(muzzlePositionable, movementModel, data.config.weaponConfig);
            
            SpaceshipFacade facade = CreateComponent<SpaceshipFacade>(
                movementModel,
                movable,
                rotatable,
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