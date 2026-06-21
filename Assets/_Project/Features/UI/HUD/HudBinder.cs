using Plugins.MVVM.Binders;

namespace _Project.Features.UI.HUD
{
    public class HudBinder : IBinder
    {
        private readonly HudViewModel _hudViewModel;
        private readonly HudView _hudView;


        public HudBinder(HudViewModel hudViewModel, HudView hudView)
        {
            _hudViewModel = hudViewModel;
            _hudView = hudView;
        }
        
        public void Bind()
        {
            _hudViewModel.HealthView.OnChanged += _hudView.SetHp;
            _hudViewModel.ScoreView.OnChanged += _hudView.SetCurrentScore;
            _hudViewModel.PositionView.OnChanged += _hudView.SetPosition;
            _hudViewModel.RotationAngleView.OnChanged += _hudView.SetRotationAngle;
            _hudViewModel.SpeedView.OnChanged += _hudView.SetSpeed;
            _hudViewModel.LaserBeamsView.OnChanged += _hudView.SetLaserBeams;
            _hudViewModel.LaserRechargeTime.OnChanged += _hudView.SetLaserRechargeTime;
        }

        public void Unbind()
        {
            _hudViewModel.HealthView.OnChanged -= _hudView.SetHp;
            _hudViewModel.ScoreView.OnChanged -= _hudView.SetCurrentScore;
            _hudViewModel.PositionView.OnChanged -= _hudView.SetPosition;
            _hudViewModel.RotationAngleView.OnChanged -= _hudView.SetRotationAngle;
            _hudViewModel.SpeedView.OnChanged -= _hudView.SetSpeed;
            _hudViewModel.LaserBeamsView.OnChanged -= _hudView.SetLaserBeams;
            _hudViewModel.LaserRechargeTime.OnChanged -= _hudView.SetLaserRechargeTime;
        }
    }
}