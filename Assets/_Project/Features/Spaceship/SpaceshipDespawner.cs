using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;
using _Project.Features.Spaceship.SpaceshipClone;

namespace _Project.Features.Spaceship
{
    public class SpaceshipDespawner : IDisposable
    {
        private readonly IFactory<SpaceshipSpawnData, SpaceshipFacade> _mainSpaceshipFactory;
        private readonly IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> _cloneSpaceshipFactory;
        private readonly Storage<SpaceshipFacade> _mainSpaceshipStorage;
        private readonly Storage<SpaceshipCloneFacade> _cloneSpaceshipStorage;
        private readonly ISignalBus _signalBus;


        public SpaceshipDespawner(
            IFactory<SpaceshipSpawnData, SpaceshipFacade> mainSpaceshipFactory,
            IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> cloneSpaceshipFactory,
            Storage<SpaceshipFacade> mainSpaceshipStorage,
            Storage<SpaceshipCloneFacade> cloneSpaceshipStorage,
            ISignalBus signalBus)
        {
            _mainSpaceshipFactory =  mainSpaceshipFactory;
            _cloneSpaceshipFactory = cloneSpaceshipFactory;
            _mainSpaceshipStorage = mainSpaceshipStorage;
            _cloneSpaceshipStorage = cloneSpaceshipStorage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<SpaceshipFacade> signal)
        {
            var spaceshipToDespawn = signal.facade;
            _mainSpaceshipFactory.Release((SpaceshipFacade)spaceshipToDespawn);
            _mainSpaceshipStorage.Remove((SpaceshipFacade)spaceshipToDespawn);

            while (_cloneSpaceshipStorage.TryGetFirst(out var clone))
            {
                _cloneSpaceshipFactory.Release(clone);
                _cloneSpaceshipStorage.Remove(clone);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);;
        }
    }
}