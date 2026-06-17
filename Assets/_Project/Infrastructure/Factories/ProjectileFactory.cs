using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Hit;
using _Project.Features.Spaceship.Weapon.Projectile;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class ProjectileFactory : AbstractFacadeFactory<ProjectileSpawnData, ProjectileFacade, MovableView>
    {
        public ProjectileFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : 
            base(instantiator, prefab, parentTransform) { }

        public override ProjectileFacade Create(ProjectileSpawnData data)
        {
            MovableView view = _pool.Get();
            
            InitialMovementData initialMovementData = data.initialMovementData;
            MovementModel movementModel = CreateComponent<MovementModel>(initialMovementData);
            IDrawable drawable = view;
            drawable.Setup(data.initialMovementData.initialPosition, 0);
            IHitSource hitSource = view.GetComponent<IHitSource>();
            IMovable movable = CreateComponent<ProjectileMovementController>(movementModel);
            BoundsChecker boundsChecker = CreateComponent<BoundsChecker>(movementModel);
            ProjectileFacade facade = CreateComponent<ProjectileFacade>(
                data.aliveTime,
                movementModel,
                drawable,
                hitSource,
                movable,
                boundsChecker);
            return facade;
            
        }
    }
}