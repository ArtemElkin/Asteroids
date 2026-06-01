using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.SpaceshipClone;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipCloneFactory : Core.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade>
    {
        private readonly IInstantiator _instantiator;
        private readonly Storage<SpaceshipFacade> _mainSpaceshipStorage;
        private readonly CustomPool<MovableView> _viewPool;
        
        
        public SpaceshipCloneFactory(
            IInstantiator instantiator,
            Storage<SpaceshipFacade> mainSpaceshipStorage,
            MovableView spaceshipPrefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _mainSpaceshipStorage = mainSpaceshipStorage;
            
            _viewPool = new CustomPool<MovableView>(instantiator, spaceshipPrefab, defaultParentTransform: parentTransform);
        }
        
        public SpaceshipCloneFacade Create(SpaceshipCloneSpawnData data)
        {
            if (_mainSpaceshipStorage.TryGetFirst(out var mainSpaceship))
            {

                var initialMovementData = new  InitialMovementData(data.offsetFromMainSpaceship, 0);
                var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
                
                var view = _viewPool.Get();
                view.Setup(movementModel);
                
                var mainSpaceshipPositionable = mainSpaceship.GetPositionable();
                var mainSpaceshipRotatable = mainSpaceship.GetRotatable();
                var spaceshipClone = _instantiator.Instantiate<SpaceshipCloneFacade>(new object[]
                {
                    view,
                    movementModel,
                    mainSpaceshipPositionable,
                    mainSpaceshipRotatable,
                    data.offsetFromMainSpaceship
                });
                return spaceshipClone;
            }
            
            Debug.LogError($"Main Spaceship not found for SpaceshipCloneFactory.Create()");
            return null;
            
        }

        public void Release(SpaceshipCloneFacade facade)
        {
            var drawable = facade.GetDrawable();
            var view = (MovableView)drawable;
            view.Reset();
            _viewPool.Release(view);
            facade.Dispose();
        }
    }
}