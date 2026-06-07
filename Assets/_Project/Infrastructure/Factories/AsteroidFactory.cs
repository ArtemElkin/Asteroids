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
            MovableView view = _viewPool.Get();
            view.transform.localScale = new Vector3(data.radius, data.radius, 1f);
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IHitable hitable = view.GetComponent<IHitable>();
            IMovable movable = CreateComponent<AsteroidMovementController>(movementModel);
            IBouncable bouncable = CreateComponent<BounceController>(movementModel);
            bool isFragment = data.fragmentsCount == 0;
            bool enteredGameAreaOnSpawn = isFragment;
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel, enteredGameAreaOnSpawn);
            AsteroidDestructor destructor = CreateComponent<AsteroidDestructor>(data.fragmentsCount, movementModel);
            AsteroidFacade facade = CreateComponent<AsteroidFacade>(
                movementModel,
                movable,
                bouncable,
                boundsChecker,
                drawable,
                collidable,
                hitable,
                destructor);
            return facade;
        }
    }
}