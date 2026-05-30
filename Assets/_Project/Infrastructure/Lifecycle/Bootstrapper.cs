using System;
using _Project.Core.Ads;
using _Project.Core.Config;
using _Project.Core.Player;
using _Project.Core.Save;
using _Project.Core.Signals;
using _Project.Infrastructure.Services;

namespace _Project.Infrastructure.Lifecycle
{
    public class Bootstrapper : IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly SceneLoadService _sceneLoadService;
        private readonly ISaveService _saveService;
        private readonly IAdsService _adsService;
        private readonly IConfigProvider _configProvider;
        private readonly ISignalBus _signalBus;
        
        
        public Bootstrapper(
            SceneLoadService sceneLoadService,
            ISaveService saveService,
            PlayerModel playerModel,
            IAdsService  adsService,
            IConfigProvider configProvider,
            ISignalBus  signalBus)
        {
            _sceneLoadService = sceneLoadService;
            _saveService = saveService;
            _playerModel = playerModel;
            _adsService = adsService;
            _configProvider = configProvider;
            _signalBus = signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
        }

        private void Initialize()
        {
            var playerSave = _saveService.Load<PlayerSave>();
            if (playerSave != null)
            {
                _playerModel.LoadSave(playerSave);
            }

            var adsConfig = _configProvider.GetConfig<AdUnitsIdsConfig>("AdUnitsIdsConfig");
            _adsService.Initialize(adsConfig);
            
            _sceneLoadService.LoadMenuScene();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
        }
    }
}
