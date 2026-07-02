using _Project.Features.UI.Common.Binders;
using Plugins.MVVM;
using Zenject;

namespace _Project.Infrastructure.DI.UI
{
    public class UICommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ButtonBinder>();
            BinderFactory.RegisterBinder<SliderBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
            BinderFactory.RegisterBinder<ViewSetterBinder<int>>();
        }
    }
}