using _Project.Infrastructure.Analytics;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAnalyticsService();
        }
        
        private void BindAnalyticsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockAnalyticsService>()
                .AsSingle()
                .NonLazy();
#else
            Container
                .BindInterfacesTo<FirebaseAnalyticsService>()
                .AsSingle()
                .NonLazy();
#endif
        }
    }
}