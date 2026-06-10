using System;
using _Project.Core.EventBus;
using UnityEngine.SceneManagement;

namespace _Project.Infrastructure.UnityServices
{
    public class SceneLoadService : IDisposable
    {
        private const string GameplaySceneName = "Game";
        private const string MainMenuSceneName = "MainMenu";
        private readonly IEventBus _eventBus;
        
        
        public SceneLoadService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<StartGameClickedEvent>(LoadGameScene);
            _eventBus.Subscribe<MenuClickedEvent>(LoadMenuScene);
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
            _eventBus.Unsubscribe<StartGameClickedEvent>(LoadGameScene);
            _eventBus.Unsubscribe<MenuClickedEvent>(LoadMenuScene);
        }
    }
}