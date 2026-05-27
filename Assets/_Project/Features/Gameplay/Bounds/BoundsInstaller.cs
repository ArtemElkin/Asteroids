using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<OutOfBoundsSignal>();

            BindBoundsService();
            BindBoundsWarper();
        }

        private void BindBoundsService()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsService>()
                .AsSingle();
        }

        private void BindBoundsWarper()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsWarper>()
                .AsSingle();
        }
    }
}