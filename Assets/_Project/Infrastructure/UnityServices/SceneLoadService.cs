using System;
using _Project.Core.EventBus;
using _Project.Core.Services;
using _Project.Features.UI.Common.Events;
using UnityEngine.SceneManagement;

namespace _Project.Infrastructure.UnityServices
{
    public class SceneLoadService : ISceneLoadService, IDisposable
    {
        private const string GameplaySceneName = "Game";
        private const string MainMenuSceneName = "MainMenu";
        private readonly IEventBus _eventBus;
        
        
        public SceneLoadService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<StartGameClickedEvent>(LoadGameScene);
            _eventBus.Subscribe<MainMenuClickedEvent>(LoadMenuScene);
        }

        public void LoadGameScene()
        {
            LoadScene(GameplaySceneName);
        }

        public void LoadMenuScene()
        {
            LoadScene(MainMenuSceneName);
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<StartGameClickedEvent>(LoadGameScene);
            _eventBus.Unsubscribe<MainMenuClickedEvent>(LoadMenuScene);
        }
    }
}