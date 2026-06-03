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
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IHitable hitable = view.GetComponent<IHitable>();
            IMovable movable = CreateComponent<BaseMovementController>(movementModel);
            IBouncable bouncable = CreateComponent<BounceController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel, movable);
            AsteroidFacade facade = CreateComponent<AsteroidFacade>(
                movable,
                bouncable,
                boundsChecker,
                drawable,
                collidable,
                hitable);
            return facade;
        }
    }
}