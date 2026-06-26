using System;
using _Project.Core.EventBus;
using _Project.Core.Player;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Events;
using Plugins.MVVM.Attributes;
using UniRx;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.UI.Gameplay.HUD
{
    public class HudViewModel : IDisposable
    {
        private SpaceshipReadOnlyInfo _info;
        private readonly PlayerModel _playerModel;
        private readonly IEventBus _eventBus;

        public ReactiveProperty<int> Health = new();
        [Data("Score")]
        public ReactiveProperty<string> Score = new();
        [Data("Position")]
        public ReactiveProperty<string> Position = new();
        [Data("RotationAngle")]
        public ReactiveProperty<string> RotationAngle = new();
        [Data("Speed")]
        public ReactiveProperty<string> Speed = new();
        [Data("LaserBeams")]
        public ReactiveProperty<string> LaserBeams = new();
        [Data("LaserRechargeTime")]
        public ReactiveProperty<string> LaserRechargeTime = new();


        public HudViewModel(PlayerModel playerModel, IEventBus eventBus)
        {
            _playerModel = playerModel;
            _eventBus = eventBus;
            
            _playerModel.CurrentScoreChanged += OnCurrentScoreChanged;
            _eventBus.Subscribe<SpaceshipSpawnedEvent>(OnSpaceshipSpawned);
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
            
            ApplyInitialValues();
        }

        private void ApplyInitialValues()
        {
            OnCurrentScoreChanged(_playerModel.CurrentScore);
            OnHealthChanged(_info.HealthModel.Hp);
            OnPositionChanged(_info.Position.Position);
            OnRotationAngleChanged(_info.Rotation.RotationAngle);
            OnVelocityChanged(_info.Velocity.Velocity);
            OnAvailableBeamCountChanged(_info.LaserWeaponState.AvailableBeamCount);
            OnRechargeTimeLeftChanged(_info.LaserWeaponState.RechargeTimeLeft);
        }

        private void OnCurrentScoreChanged(int newScore) => Score.Value = newScore.ToString();
        private void OnHealthChanged(int newHp) => Health.Value = newHp;
        private void OnPositionChanged(Vector2 newPos) => Position.Value = $"Position: [{newPos.x:F1}; {newPos.y:F1}]";
        private void OnRotationAngleChanged(float newAngle) => RotationAngle.Value = $"Rotation angle: {MathF.Abs(newAngle):F0}";
        private void OnVelocityChanged(Vector2 newVelocity) => Speed.Value = $"Speed: {newVelocity.magnitude:F0} m/s";
        private void OnAvailableBeamCountChanged(int newCount) => LaserBeams.Value = $"Laser beams: {newCount}";
        private void OnRechargeTimeLeftChanged(float timeLeft) => LaserRechargeTime.Value = $"Laser recharge time: {timeLeft:F1}";

        public void Dispose()
        {
            _eventBus.Unsubscribe<SpaceshipSpawnedEvent>(OnSpaceshipSpawned);
            _playerModel.CurrentScoreChanged -= OnCurrentScoreChanged;
            if (_info == null) return;
            _info.HealthModel.OnHpChanged -= OnHealthChanged;
            _info.Position.PositionChanged -= OnPositionChanged;
            _info.Rotation.RotationAngleChanged -= OnRotationAngleChanged;
            _info.Velocity.VelocityChanged -= OnVelocityChanged;
            _info.LaserWeaponState.AvailableBeamCountChanged -=  OnAvailableBeamCountChanged;
            _info.LaserWeaponState.RechargeTimeLeftChanged -= OnRechargeTimeLeftChanged;
        }
    }
}