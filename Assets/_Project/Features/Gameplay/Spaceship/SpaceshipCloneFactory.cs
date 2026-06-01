using _Project.Core.Physics;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipCloneFactory : Infrastructure.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade>
    {
        private readonly IInstantiator _instantiator;
        private readonly Storage<SpaceshipFacade> _mainSpaceshipStorage;
        private readonly SpaceshipView _spaceshipPrefab;
        private readonly Transform _spaceshipParentTransform;
        
        
        public SpaceshipCloneFactory(
            IInstantiator instantiator,
            Storage<SpaceshipFacade> mainSpaceshipStorage,
            SpaceshipView spaceshipPrefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _mainSpaceshipStorage = mainSpaceshipStorage;
            _spaceshipPrefab = spaceshipPrefab;
            _spaceshipParentTransform = parentTransform;
        }
        
        public SpaceshipCloneFacade Create(SpaceshipCloneSpawnData data)
        {
            if (_mainSpaceshipStorage.TryGetFirst(out var mainSpaceship))
            {

                var initialMovementData = new  InitialMovementData(data.offsetFromMainSpaceship, 0);
                var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
                
                var view = _instantiator.InstantiatePrefabForComponent<SpaceshipView>(_spaceshipPrefab, _spaceshipParentTransform, new object[]
                {
                    (IReadOnlyPositionable) movementModel,
                    (IReadOnlyRotatable) movementModel
                });
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
    }
}