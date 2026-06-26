using Plugins.MVVM.Binders;
using UniRx;

namespace _Project.Features.UI.Gameplay.HUD.Binders
{
    public class HpBinder : IBinder
    {
        private readonly HudViewModel _hudViewModel;
        private readonly HudView _hudView;
        private readonly CompositeDisposable _disposables = new();


        public HpBinder(HudViewModel hudViewModel, HudView hudView)
        {
            _hudViewModel = hudViewModel;
            _hudView = hudView;
        }
        
        public void Bind()
        {
            _hudViewModel.Health
                .Subscribe(_hudView.SetHp)
                .AddTo(_disposables);
        }

        public void Unbind()
        {
            _disposables.Clear();
        }
    }
}