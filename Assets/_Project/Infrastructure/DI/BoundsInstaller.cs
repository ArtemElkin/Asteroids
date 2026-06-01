using _Project.Features.Common.Bounds;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class BoundsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBoundsService();
            BindBoundsWarper();
        }

        private void BindBoundsService()
        {
            Container
                .Bind<BoundsService>()
                .AsSingle()
                .NonLazy();
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