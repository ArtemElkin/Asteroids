using _Project.Core.Physics;
using _Project.Features.Asteroid;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Spaceship;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class AsteroidFactory : Core.Factories.IFactory<AsteroidSpawnData, AsteroidFacade>
    {
        private readonly CustomPool<MovableView> _viewPool;
        private readonly IInstantiator _instantiator;


        public AsteroidFactory(
            IInstantiator instantiator,
            MovableView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _viewPool = new CustomPool<MovableView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }

        public AsteroidFacade Create(AsteroidSpawnData data)
        {
            var initialMovementData = new InitialMovementData(data.initialPosition, data.initialSpeed, data.initialDirection);
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var movementController = _instantiator.Instantiate<AsteroidMovementController>(new object[] { movementModel });

            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movementController
            });

            var view = _viewPool.Get();
            view.Setup(movementModel);
            
            var collisionHandler = view.GetComponent<AsteroidCollisionHandler>();
            
            var asteroid = _instantiator.Instantiate<AsteroidFacade>(new object[]
            {
                movementController,
                boundsChecker,
                view,
                collisionHandler
            });

            return asteroid;
        }

        public void Release(AsteroidFacade asteroidFacade)
        {
            var drawable = asteroidFacade.GetDrawable();
            var view = (MovableView)drawable;
            view.Reset();
            _viewPool.Release(view);
            asteroidFacade.Dispose();
        }
    }
}