using System;
using _Project.Core.Ads;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Features.UI.Common.Events;

namespace _Project.Features.Ads
{
    public class GameplayAdsController : IDisposable
    {
        private const int DeathsPerAdInterval = 3;
        private int _deathsFromLastAd;
        private readonly IEventBus _eventBus;
        private readonly IGameStateService _gameStateService;
        private readonly IAdsService _adsService;

        
        public GameplayAdsController(IEventBus eventBus, IGameStateService gameStateService, IAdsService adsService)
        {
            _eventBus = eventBus;
            _gameStateService = gameStateService;
            _adsService = adsService;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
            _eventBus.Subscribe<MainMenuClickedEvent>(OnMainMenuClicked);
            _deathsFromLastAd = 0;
        }

        private void OnGameStateChanged(GameState gameState, TransitionType transitionType)
        {
            switch (gameState)
            {
                case GameState.Running:
                    _adsService.HideBanner();
                    break;
                case GameState.Paused:
                    _adsService.ShowBanner();
                    break;
                case GameState.GameOver:
                    _adsService.ShowBanner();
                    _deathsFromLastAd++;
                    if (_deathsFromLastAd >= DeathsPerAdInterval)
                    {
                        _adsService.ShowInterstitial();
                        _deathsFromLastAd = 0;
                    }
                    break;
            }
        }

        private void OnMainMenuClicked()
        {
            _adsService.HideBanner();
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
            _eventBus.Unsubscribe<MainMenuClickedEvent>(OnMainMenuClicked);
        }
    }
}