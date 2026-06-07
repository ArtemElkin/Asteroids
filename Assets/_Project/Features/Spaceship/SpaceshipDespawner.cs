using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Clone;
using _Project.Features.Common.Signals;

namespace _Project.Features.Spaceship
{
    public class SpaceshipDespawner : IDisposable
    {
        private readonly IFactory<SpaceshipSpawnData, SpaceshipFacade> _mainSpaceshipFactory;
        private readonly IFactory<CloneSpawnData, CloneFacade<SpaceshipFacade>> _cloneFactory;
        private readonly Storage<SpaceshipFacade> _mainSpaceshipStorage;
        private readonly ISignalBus _signalBus;


        public SpaceshipDespawner(
            IFactory<SpaceshipSpawnData, SpaceshipFacade> mainSpaceshipFactory,
            IFactory<CloneSpawnData, CloneFacade<SpaceshipFacade>> cloneFactory,
            Storage<SpaceshipFacade> mainSpaceshipStorage,
            ISignalBus signalBus)
        {
            _mainSpaceshipFactory =  mainSpaceshipFactory;
            _cloneFactory = cloneFactory;
            _mainSpaceshipStorage = mainSpaceshipStorage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<SpaceshipFacade> signal)
        {
            var spaceshipToDespawn = (SpaceshipFacade)signal.facade;
            _mainSpaceshipFactory.Release(spaceshipToDespawn);
            _mainSpaceshipStorage.Remove(spaceshipToDespawn);
            _signalBus.Fire(new CloneDespawnRequestedSignal<SpaceshipFacade>(spaceshipToDespawn));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);;
        }
    }
}