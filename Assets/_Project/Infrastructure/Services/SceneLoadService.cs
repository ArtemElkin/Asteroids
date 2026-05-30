using System;
using _Project.Core.Signals;
using UnityEngine.SceneManagement;

namespace _Project.Infrastructure.Services
{
    public class SceneLoadService : IDisposable
    {
        private const string GameplaySceneName = "Game";
        private const string MainMenuSceneName = "MainMenu";
        private readonly ISignalBus _signalBus;
        
        
        public SceneLoadService(ISignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<StartGameClickedSignal>(LoadGameScene);
            _signalBus.Subscribe<MenuClickedSignal>(LoadMenuScene);
        }
        
        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadGameScene()
        {
            LoadScene(GameplaySceneName);
        }

        public void LoadMenuScene()
        {
            LoadScene(MainMenuSceneName);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameClickedSignal>(LoadGameScene);
            _signalBus.Unsubscribe<MenuClickedSignal>(LoadMenuScene);
        }
    }
}