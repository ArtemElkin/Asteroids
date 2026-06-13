using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.GameLifecycle
{
    public class GameplayStarter : MonoBehaviour
    {
        private IEventBus _eventBus;


        private void Start()
        {
            _eventBus.Publish<GameInitializeEvent>();
            _eventBus.Publish<GameStartEvent>();
        }

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}