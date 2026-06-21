using System;
using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Events;
using Plugins.MVVM;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.UI.HUD
{
    public class HudViewModel : IDisposable
    {
        private SpaceshipReadOnlyInfo _info;
        private readonly PlayerModel _playerModel;
        private readonly IEventBus _eventBus;

        public ReactiveProperty<string> ScoreView = new();
        public ReactiveProperty<int> HealthView = new();
        public ReactiveProperty<string> PositionView = new();
        public ReactiveProperty<string> RotationAngleView = new();
        public ReactiveProperty<string> SpeedView = new();
        public ReactiveProperty<string> LaserBeamsView = new();
        public ReactiveProperty<string> LaserRechargeTime = new();


        public HudViewModel(PlayerModel playerModel, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<SpaceshipSpawnedEvent>(OnSpaceshipSpawned);
            _playerModel = playerModel;
            _playerModel.CurrentScoreChanged += OnCurrentScoreChanged;
        }

        public void Init()
        {
            OnCurrentScoreChanged(_playerModel.CurrentScore);
            OnHealthChanged(_info.HealthModel.Hp);
            OnPositionChanged(_info.Position.Position);
            OnRotationAngleChanged(_info.Rotation.RotationAngle);
            OnVelocityChanged(_info.Velocity.Velocity);
            OnAvailableBeamCountChanged(_info.LaserWeaponState.AvailableBeamCount);
            OnRechargeTimeLeftChanged(_info.LaserWeaponState.RechargeTimeLeft);
        }

        private void OnSpaceshipSpawned(SpaceshipSpawnedEvent @event)
        {
            _info = @event.Info;
            _info.HealthModel.OnHpChanged += OnHealthChanged;
            _info.Position.PositionChanged += OnPositionChanged;
            _info.Rotation.RotationAngleChanged += OnRotationAngleChanged;
            _info.Velocity.VelocityChanged += OnVelocityChanged;
            _info.LaserWeaponState.AvailableBeamCountChanged +=  OnAvailableBeamCountChanged;
            _info.LaserWeaponState.RechargeTimeLeftChanged += OnRechargeTimeLeftChanged;
            
            Init();
        }

        private void OnCurrentScoreChanged(int newScore)
        {
            ScoreView.Value = newScore.ToString();
        }

        private void OnHealthChanged(int newHp)
        {
            HealthView.Value = newHp;
        }

        private void OnPositionChanged(Vector2 newPosition)
        {
            PositionView.Value = $"Position: [{newPosition.x:F1}; {newPosition.y:F1}]";
        }

        private void OnRotationAngleChanged(float newAngle)
        {
            RotationAngleView.Value = $"Rotation angle: {MathF.Abs(newAngle):F0}";
        }

        private void OnVelocityChanged(Vector2 newVelocity)
        {
            SpeedView.Value = $"Speed: {newVelocity.magnitude:F0} m/s";
        }

        private void OnAvailableBeamCountChanged(int newCount)
        {
            LaserBeamsView.Value = $"Laser beams: {newCount}";
        }

        private void OnRechargeTimeLeftChanged(float timeLeft)
        {
            LaserRechargeTime.Value = $"Laser recharge time: {timeLeft:F1}";
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SpaceshipSpawnedEvent>(OnSpaceshipSpawned);
            _playerModel.CurrentScoreChanged -= OnCurrentScoreChanged;
            _info.HealthModel.OnHpChanged -= OnHealthChanged;
            _info.Position.PositionChanged -= OnPositionChanged;
            _info.Rotation.RotationAngleChanged -= OnRotationAngleChanged;
            _info.Velocity.VelocityChanged -= OnVelocityChanged;
            _info.LaserWeaponState.AvailableBeamCountChanged -=  OnAvailableBeamCountChanged;
            _info.LaserWeaponState.RechargeTimeLeftChanged -= OnRechargeTimeLeftChanged;
        }
    }
}