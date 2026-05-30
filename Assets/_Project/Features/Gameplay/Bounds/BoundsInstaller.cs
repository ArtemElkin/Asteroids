using _Project.Features.Gameplay.Common;
using _Project.Features.Gameplay.Signals;
using Zenject;

namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsInstaller : Installer<BoundsInstaller>
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