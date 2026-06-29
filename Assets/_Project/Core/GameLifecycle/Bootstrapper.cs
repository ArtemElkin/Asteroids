using System.Collections.Generic;
using _Project.Core.Ads;
using _Project.Core.Config;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Core.StaticData;

namespace _Project.Core.GameLifecycle
{
    public class Bootstrapper
    {
        private readonly List<ISaveBootstrap> _saveBootstraps;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IAdsService _adsService;
        private readonly IConfigProvider _configProvider;
        
        
        public Bootstrapper(
            List<ISaveBootstrap> saveBootstraps,
            ISceneLoadService sceneLoadService,
            IAdsService  adsService,
            IConfigProvider configProvider)
        {
            _saveBootstraps = saveBootstraps;
            _sceneLoadService = sceneLoadService;
            _adsService = adsService;
            _configProvider = configProvider;
            
            Initialize();
        }

        private void Initialize()
        {
            foreach (var saveBootstrap in _saveBootstraps)
            {
                saveBootstrap.LoadOnBootstrap();
            }

            var adsConfig = _configProvider.GetConfig<AdUnitsIdsConfig>(FileNames.Config.AdUnits);
            _adsService.Initialize(adsConfig);
            
            _sceneLoadService.LoadMenuScene();
        }
    }
}
