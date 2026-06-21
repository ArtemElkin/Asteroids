using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Features.Asteroid;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Features.Common.Hit;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class AsteroidFactory : AbstractFacadeFactory<AsteroidSpawnData, AsteroidFacade, MovableView>
    {
        public AsteroidFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override AsteroidFacade Create(AsteroidSpawnData data)
        {
            MovableView view = _pool.Get();
            view.transform.localScale = new Vector3(data.radius, data.radius, 1f);
            MovementModel movementModel = CreateComponent<MovementModel>(data.initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel);
            IHitable hitable = view.GetComponent<IHitable>();
            IMovable movable = CreateComponent<AsteroidMovementController>(movementModel);
            bool isFragment = data.fragmentsCount == 0;
            EnemyType enemyType = isFragment ? EnemyType.AsteroidFragment : EnemyType.Asteroid;
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
                enemyType,
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