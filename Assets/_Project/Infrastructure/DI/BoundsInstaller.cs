using _Project.Features.Gameplay.Bounds;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class BoundsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBoundsService();
            BindBoundsChecker();
            BindBoundsWarper();
        }

        private void BindBoundsService()
        {
            Container
                .Bind<BoundsService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindBoundsChecker()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsChecker>()
                .AsTransient();
        }

        private void BindBoundsWarper()
        {
            Container
                .BindInterfacesAndSelfTo<BoundsWarper>()
                .AsSingle()
                .NonLazy();
        }
    }
}