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
            var initialMovementData = new InitialMovementData(data.initialPosition, 0);
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var view = _viewPool.Get();

            IDrawable drawable = view;
            drawable.Setup(movementModel);
            
            ICollidable collidable = view.GetComponent<ICollidable>();
            
            IMovable movable = _instantiator.Instantiate<SpaceshipMovementController>(new object[]
            {
                movementModel,
                data.movementConfig
            });
            
            IRotatable rotatable = _instantiator.Instantiate<SpaceshipRotationController>(new object[] { movementModel });
            
            IBouncable bouncable = _instantiator.Instantiate<BounceController>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movable,
            });
            
            var healthModel = _instantiator.Instantiate<HealthModel>(new object[] { data.initialHp });
            var healthController = _instantiator.Instantiate<HealthController>(new object[] { healthModel });
            
            var spaceship = _instantiator.Instantiate<SpaceshipFacade>(new object[]
            {
                movementModel,
                movable,
                rotatable,
                bouncable,
                boundsChecker,
                drawable,
                healthController,
                collidable
            });
            
            return spaceship;
        }
    }
}