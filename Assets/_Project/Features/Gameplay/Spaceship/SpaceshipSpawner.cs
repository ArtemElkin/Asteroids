using System;
using _Project.Core.Signals;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipSpawner : IInitializable, IDisposable
    {
        private readonly SpaceshipCloneComponent _spaceshipClonePrefab;
        private readonly SpaceshipComponent _spaceshipPrefab;
        private readonly IInstantiator _instantiator;
        private readonly SignalBus _signalBus;
        private readonly ScreenService _screenService;

        
        public SpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            SpaceshipCloneComponent spaceshipClonePrefab,
            IInstantiator instantiator,
            SignalBus signalBus,
            ScreenService screenService)
        {
            _spaceshipPrefab = spaceshipPrefab;
            _spaceshipClonePrefab = spaceshipClonePrefab;
            _instantiator = instantiator;
            _signalBus = signalBus;
            _screenService = screenService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
            SpawnSpaceshipClones();
        }

        private void SpawnSpaceship()
        {
            _instantiator.InstantiatePrefabForComponent<SpaceshipComponent>(_spaceshipPrefab);
        }

        private void SpawnSpaceshipClones()
        {
            var width = _screenService.ScreenWidth;
            var height = _screenService.ScreenHeight;

            Vector2[] cloneOffsets = 
            {
                new (0, height),
                new (width, height),
                new (width, 0),
                new (width, -height),
                new (0, -height),
                new (-width, -height),
                new (-width, 0),
                new (-width, height)
            };

            foreach (var offset in cloneOffsets)
            {
                var clone = _instantiator.InstantiatePrefabForComponent<SpaceshipCloneComponent>(_spaceshipClonePrefab);
                clone.Setup(offset);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
    }
}