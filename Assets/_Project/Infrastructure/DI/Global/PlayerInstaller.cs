using _Project.Core.Player;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerModel();
            BindPlayerSaveController();
        }
        
        private void BindPlayerModel()
        {
            Container
                .Bind<PlayerModel>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerSaveController()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerSaveController>()
                .AsSingle()
                .NonLazy();
        }
    }
    
}