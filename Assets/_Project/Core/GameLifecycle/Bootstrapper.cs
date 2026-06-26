using System;
using _Project.Core.Ads;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using _Project.Core.Player;
using _Project.Core.Save;
using _Project.Core.Services;

namespace _Project.Core.GameLifecycle
{
    public class Bootstrapper : IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ISaveService _saveService;
        private readonly IAdsService _adsService;
        private readonly IConfigProvider _configProvider;
        private readonly IEventBus _eventBus;
        
        
        public Bootstrapper(
            ISceneLoadService sceneLoadService,
            ISaveService saveService,
            PlayerModel playerModel,
            IAdsService  adsService,
            IConfigProvider configProvider,
            IEventBus  eventBus)
        {
            _sceneLoadService = sceneLoadService;
            _saveService = saveService;
            _playerModel = playerModel;
            _adsService = adsService;
            _configProvider = configProvider;
            _eventBus = eventBus;
            _eventBus.Subscribe<SceneInitializeEvent>(Initialize);
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
            
            _sceneLoadService.LoadGameScene();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SceneInitializeEvent>(Initialize);
        }
    }
}
