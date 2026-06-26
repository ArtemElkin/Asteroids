using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.GameLifecycle
{
    public class SceneInitializer : MonoBehaviour
    {
        private IEventBus _eventBus;


        private void Awake()
        {
            _eventBus.Publish<SceneInitializeEvent>();
        }

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}