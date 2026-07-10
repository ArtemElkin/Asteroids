using System.Collections.Generic;
using _Project.Core.GameLifecycle;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Ads;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Collision;
using _Project.Features.Common.Collision.Resolvers;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Infrastructure.Audio;
using _Project.Infrastructure.GameLifecycle;
using _Project.Infrastructure.Input;
using _Project.Infrastructure.Input.MobileInput;
using _Project.Infrastructure.Input.StandaloneInput;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private bool _useMobileInputInEditor;
        [SerializeField] private GameObject _standaloneInputHandler;
        [SerializeField] private GameObject _mobileInputHandler;
        [SerializeField] private Camera _camera;
        
        
        public override void InstallBindings()
        {
            BindScreenService(_camera);
#if UNITY_EDITOR
            if (_useMobileInputInEditor)
                BindMobileInput(_mobileInputHandler);
            else
                BindStandaloneInput(_standaloneInputHandler);
#elif UNITY_ANDROID || UNITY_IOS
            BindMobileInput(_mobileInputHandler);
#else
            BindStandaloneInput(_standaloneInputHandler);
#endif
            BindCollisionService();

            BindGameStateService();
            
            BindPositionGenerator();
            BindGameplayStarter();
            BindPauseController();
            BindAudioPauseController();
            BindWorldResetService();
            BindRestartController();
            
            BindGameplayAdsController();
            BindEnemyAwardsService();
            
            BindBoundsService();
            BindBoundsWarper();
        }
        
        private void BindScreenService(Camera mainCamera)
        {
            Container
                .Bind<IScreenService>()
                .To<ScreenService>()
                .AsSingle()
                .WithArguments(mainCamera)
                .NonLazy();
        }
        
        private void BindStandaloneInput(GameObject inputHandler)
        {
            Container
                .BindInterfacesTo<StandaloneInputHandler>()
                .FromComponentOn(inputHandler)
                .AsSingle()
                .NonLazy();
            
            inputHandler.SetActive(true);
        }

        private void BindMobileInput(GameObject inputHandler)
        {
            Container
                .BindInterfacesTo<MobileInputHandler>()
                .FromComponentOn(inputHandler)
                .AsSingle()
                .NonLazy();
            
            inputHandler.SetActive(true);
        }

        private void BindCollisionService()
        {
            Container
                .BindInterfacesTo<SimpleReflectionCollisionResolver>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<ElasticCollisionResolver>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<CollisionService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameStateService()
        {
            Container
                .BindInterfacesAndSelfTo<GameStateService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPositionGenerator()
        {
            Container
                .Bind<PositionGenerator>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindGameplayAdsController()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayAdsController>()
                .AsSingle();
        }

        private void BindGameplayStarter()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayStarter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseController()
        {
            Container
                .BindInterfacesAndSelfTo<PauseService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAudioPauseController()
        {
            Container
                .BindInterfacesAndSelfTo<AudioPauseController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindWorldResetService()
        {
            Container
                .BindInterfacesAndSelfTo<WorldResetService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRestartController()
        {
            Container
                .BindInterfacesAndSelfTo<RestartService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyAwardsService()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyAwardsService>()
                .AsSingle()
                .NonLazy();
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