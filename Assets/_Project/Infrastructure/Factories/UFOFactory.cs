using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Hit;
using _Project.Features.UFO;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class UFOFactory : AbstractFacadeFactory<UFOSpawnData, UFOFacade, MovableView>
    {
        public UFOFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) :
            base(instantiator, prefab, parentTransform) { }

        public override UFOFacade Create(UFOSpawnData data)
        {
            MovableView view = _pool.Get();
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(movementModel, false);
            IHitable hitable = view.GetComponent<IHitable>();
            UFODeathHandler deathHandler = CreateComponent<UFODeathHandler>();
            IMovable movable = CreateComponent<UFOMovementController>(movementModel, data.speed);
            IRotatable rotatable = CreateComponent<UFORotationController>(movementModel);
            UFOTargetFollower targetFollower = CreateComponent<UFOTargetFollower>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            UFOFacade facade = CreateComponent<UFOFacade>(
                movementModel,
                movable,
                rotatable,
                targetFollower,
                boundsChecker,
                view,
                collidable,
                hitable,
                deathHandler);
            return facade;
        }
    }
}