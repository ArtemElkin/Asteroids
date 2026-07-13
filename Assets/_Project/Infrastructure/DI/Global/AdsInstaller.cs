using _Project.Infrastructure.Ads;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAdsService();
        }
        
        private void BindAdsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockAdsService>()
                .AsSingle()
                .NonLazy();
#else
            Container
                .BindInterfacesTo<YandexAdsService>()
                .AsSingle()
                .NonLazy();
#endif
        }
    }
}