using _Project.Core.Physics;
using _Project.Features.Asteroid;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class AsteroidFactory : AbstractFactory<AsteroidSpawnData, AsteroidFacade>
    {
        public AsteroidFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override AsteroidFacade Create(AsteroidSpawnData data)
        {
            var initialMovementData = new InitialMovementData(data.initialPosition, data.initialSpeed, data.initialDirection);
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var view = _viewPool.Get();
            
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            
            ICollidable collidable = view.GetComponent<ICollidable>();
            
            IHitable hitable = view.GetComponent<IHitable>();
            
            IMovable movable = _instantiator.Instantiate<BaseMovementController>(new object[] { movementModel });
            
            IBouncable bouncable = _instantiator.Instantiate<BounceController>(new object[] { movementModel });

            BoundsChecker boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movable
            });
            
            var asteroid = _instantiator.Instantiate<AsteroidFacade>(new object[]
            {
                movable,
                bouncable,
                boundsChecker,
                drawable,
                collidable,
                hitable
            });

            return asteroid;
        }
    }
}