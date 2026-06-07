using System;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Common.Clone
{
    public class CloneSpawner<TOriginFacade> : IDisposable where TOriginFacade : IFacade
    {
        private readonly IFactory<CloneSpawnData, CloneFacade<TOriginFacade>> _cloneFactory;
        private readonly CloneStorage<TOriginFacade> _storage;
        private readonly ISignalBus _signalBus;
        private readonly IScreenService _screenService;


        public CloneSpawner(
            IFactory<CloneSpawnData, CloneFacade<TOriginFacade>> cloneFactory,
            CloneStorage<TOriginFacade> storage,
            ISignalBus signalBus,
            IScreenService screenService)
        {
            _cloneFactory = cloneFactory;
            _storage = storage;
            _signalBus = signalBus;
            _screenService = screenService;
            _signalBus.Subscribe<CloneSpawnRequestedSignal<TOriginFacade>>(OnClonesSpawnRequested);
        }

        private void OnClonesSpawnRequested(CloneSpawnRequestedSignal<TOriginFacade> signal)
        {
            var origin = signal.originFacade;
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
                var spawnData = new CloneSpawnData(offset, origin.GetPositionable(), origin.GetRotationable(), origin.GetDrawable());
                var clone = _cloneFactory.Create(spawnData);
                _storage.AddClone(origin, clone);
            }

            var clones = _storage.GetAllClones(origin);
            foreach (var clone in clones)
            {
                clone.GetDrawable().Hide();
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CloneSpawnRequestedSignal<TOriginFacade>>(OnClonesSpawnRequested);
        }
    }
}