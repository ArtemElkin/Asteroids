using System;
using _Project.Core.Ads;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;

namespace _Project.Features.Ads
{
    public class GameplayAdsController : IDisposable
    {
        private const int DeathsPerAdInterval = 3;
        private int _deathsFromLastAd;
        private IEventBus _eventBus;
        private IAdsService _adsService;

        
        public GameplayAdsController(IEventBus eventBus, IAdsService adsService)
        {
            _eventBus = eventBus;
            _adsService = adsService;
            
            _eventBus.Subscribe<GameStopEvent>(OnGameOver);
            _eventBus.Subscribe<GameRestartEvent>(OnGameRestarted);
            // _eventBus.Subscribe<MenuClickedEvent>(OnMenuClicked);
            _deathsFromLastAd = 0;
        }

        private void OnGameOver()
        {
            _adsService.ShowBanner();
            _deathsFromLastAd++;
            if (_deathsFromLastAd >= DeathsPerAdInterval)
            {
                _adsService.ShowInterstitial();
                _deathsFromLastAd = 0;
            }
        }

        private void OnGameRestarted()
        {
            _adsService.HideBanner();
        }

        private void OnMenuClicked()
        {
            _adsService.HideBanner();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameStopEvent>(OnGameOver);
            _eventBus.Unsubscribe<GameRestartEvent>(OnGameRestarted);
            // _eventBus.Unsubscribe<MenuClickedEvent>(OnMenuClicked);
        }
    }
}