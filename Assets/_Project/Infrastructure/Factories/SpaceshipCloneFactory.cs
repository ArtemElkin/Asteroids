using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Common;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.SpaceshipClone;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipCloneFactory : AbstractFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade>
    {
        private readonly Storage<SpaceshipFacade> _mainSpaceshipStorage;


        public SpaceshipCloneFactory(
            IInstantiator instantiator, 
            MovableView prefab, Transform parentTransform, 
            Storage<SpaceshipFacade> mainSpaceshipStorage) : base(instantiator, prefab, parentTransform)
        {
            _mainSpaceshipStorage = mainSpaceshipStorage;
        }

        public override SpaceshipCloneFacade Create(SpaceshipCloneSpawnData data)
        {
            if (_mainSpaceshipStorage.TryGetFirst(out var mainSpaceship))
            {

                var initialMovementData = new  InitialMovementData(data.offsetFromMainSpaceship, 0);
                var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
                
                var view = _viewPool.Get();
                
                IDrawable drawable = view;
                drawable.Setup(movementModel);
                
                var positionable = mainSpaceship.GetPositionable();
                var rotationable = mainSpaceship.GetRotationable();
                var spaceshipClone = _instantiator.Instantiate<SpaceshipCloneFacade>(new object[]
                {
                    drawable,
                    movementModel,
                    positionable,
                    rotationable,
                    data.offsetFromMainSpaceship
                });
                return spaceshipClone;
            }
            
            Debug.LogError($"Main Spaceship not found for SpaceshipCloneFactory.Create()");
            return null;
            
        }
    }
}