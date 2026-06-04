using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.UFO;
using _Project.Infrastructure.UnityRender;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class UFOFactory : AbstractFactory<UFOSpawnData, UFOFacade>
    {
        public UFOFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) :
            base(instantiator, prefab, parentTransform) { }

        public override UFOFacade Create(UFOSpawnData data)
        {
            MovableView view = _viewPool.Get();
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IHitable hitable = view.GetComponent<IHitable>();
            IMovable movable = CreateComponent<UFOMovementController>(movementModel, data.speed);
            IRotatable rotatable = CreateComponent<UFORotationController>(movementModel);
            IBouncable bouncable = CreateComponent<BounceController>(movementModel);
            UFOTargetFollower targetFollower = CreateComponent<UFOTargetFollower>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel, movable);
            UFOFacade facade = CreateComponent<UFOFacade>(
                movable,
                rotatable,
                bouncable,
                targetFollower,
                boundsChecker,
                view,
                collidable,
                hitable);
            return facade;
        }
    }
}