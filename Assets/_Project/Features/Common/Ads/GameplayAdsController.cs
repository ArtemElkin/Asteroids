using System;
using _Project.Core.Ads;
using _Project.Core.Signals;

namespace _Project.Features.Common.Ads
{
    public class GameplayAdsController : IDisposable
    {
        private const int DeathsPerAdInterval = 3;
        private int _deathsFromLastAd;
        private ISignalBus _signalBus;
        private IAdsService _adsService;

        
        public GameplayAdsController(ISignalBus signalBus, IAdsService adsService)
        {
            _signalBus = signalBus;
            _adsService = adsService;
            
            _signalBus.Subscribe<GameStopSignal>(OnGameOver);
            _signalBus.Subscribe<GameRestartSignal>(OnGameRestarted);
            _signalBus.Subscribe<MenuClickedSignal>(OnMenuClicked);
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
            _signalBus.Unsubscribe<GameStopSignal>(OnGameOver);
            _signalBus.Unsubscribe<GameRestartSignal>(OnGameRestarted);
            _signalBus.Unsubscribe<MenuClickedSignal>(OnMenuClicked);
        }
    }
}