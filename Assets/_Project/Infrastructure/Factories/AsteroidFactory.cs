using _Project.Core.Physics;
using _Project.Core.Render;
using _Project.Features.Asteroid;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Infrastructure.Render;
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
            MovableView view = (MovableView)_viewPool.Get();
            view.transform.localScale = new Vector3(data.radius, data.radius, 1f);
            MovementModel movementModel = CreateComponent<MovementModel>(data.initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel);
            IHitable hitable = view.GetComponent<IHitable>();
            IMovable movable = CreateComponent<AsteroidMovementController>(movementModel);
            bool isFragment = data.fragmentsCount == 0;
            bool enteredGameAreaOnSpawn = isFragment;
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel, enteredGameAreaOnSpawn);
            AsteroidDestructor destructor = CreateComponent<AsteroidDestructor>(movementModel, data.fragmentsCount);
            IScreenWrapCloneSet screenWrapCloneSet = data.hasClones
                ? CreateComponent<ScreenWrapCloneSet<AsteroidFacade>>(
                    movementModel,
                    boundsChecker,
                    drawable)
                : new NullScreenWrapCloneSet();
            AsteroidFacade facade = CreateComponent<AsteroidFacade>(
                movementModel,
                movable,
                boundsChecker,
                drawable,
                collidable,
                hitable,
                destructor,
                screenWrapCloneSet);
            return facade;
        }
    }
}