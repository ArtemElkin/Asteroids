using System;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Core.StaticData;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Common.EnemyAwardsService
{
    public class EnemyAwardsService : IDisposable
    {
        private readonly AwardsConfig _awardsConfig;
        private readonly PlayerModel _playerModel;
        private readonly PlayerSaveController _playerSaveController;
        private readonly IEventBus _eventBus;
        
        
        public EnemyAwardsService(
            PlayerModel playerModel,
            PlayerSaveController playerSaveController,
            IConfigProvider configProvider,
            IEventBus eventBus)
        {
            _playerModel = playerModel;
            _playerSaveController = playerSaveController;
            _eventBus = eventBus;
            _awardsConfig = configProvider.GetConfig<AwardsConfig>(FileNames.Config.Awards);
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
                    _playerModel.TryUpdateMaxScore(_playerModel.CurrentScore);
                    _playerSaveController.Save();
                }
            }
        }
        
        public void Dispose()
        {
            _eventBus.Unsubscribe<EnemyDestroyedEvent>(OnEnemyDestroyed);
        }
    }
}