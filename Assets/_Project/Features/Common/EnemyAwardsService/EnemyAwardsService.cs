using System;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using _Project.Core.Player;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Common.EnemyAwardsService
{
    public class EnemyAwardsService : IDisposable
    {
        private AwardsConfig _awardsConfig;
        private readonly PlayerModel _playerModel;
        private readonly IConfigProvider _configProvider;
        private readonly IEventBus _eventBus;
        
        
        public EnemyAwardsService(
            PlayerModel playerModel,
            IConfigProvider configProvider,
            IEventBus eventBus)
        {
            _playerModel = playerModel;
            _configProvider = configProvider;
            _eventBus = eventBus;
            _eventBus.Subscribe<GameInitializeEvent>(OnGameInitialize);
            _eventBus.Subscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
        }

        private void OnEnemyDestroyed(EnemyDestroyedEvent @event)
        {
            var type =  @event.Type;
            if (_awardsConfig?.EnemyAwards != null)
            {
                if (_awardsConfig.EnemyAwards.TryGetValue(type, out var reward))
                {
                    _playerModel.IncreaseCurrentScore(reward);
                }
            }
        }

        private void OnGameInitialize()
        {
            _awardsConfig = _configProvider.GetConfig<AwardsConfig>("AwardsConfig");
        }
        
        public void Dispose()
        {
            _eventBus.Unsubscribe<GameInitializeEvent>(OnGameInitialize);
            _eventBus.Unsubscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
        }
    }
}