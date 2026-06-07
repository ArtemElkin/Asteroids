using _Project.Core.Physics;
using _Project.Features.Asteroid;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Projectile;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class ProjectileFactory : AbstractFactory<ProjectileSpawnData, ProjectileFacade>
    {
        public ProjectileFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override ProjectileFacade Create(ProjectileSpawnData data)
        {
            MovableView view = _viewPool.Get();
            
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            ICollidable collidable = view.GetComponent<ICollidable>();
            IMovable movable = CreateComponent<ProjectileMovementController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            ProjectileFacade facade = CreateComponent<ProjectileFacade>(
                movementModel,
                drawable,
                collidable,
                movable,
                boundsChecker);
            return facade;
            
        }
    }
}