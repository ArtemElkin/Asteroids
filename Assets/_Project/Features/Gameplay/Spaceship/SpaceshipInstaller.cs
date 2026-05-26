using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private SpaceshipComponent _spaceshipPrefab;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipAccelerationHandler();
            BindSpaceshipInertiaHandler();
            BindSpaceshipMovementController();
            BindSpaceshipSpawner(_spaceshipPrefab);
        }

        private void BindSpaceshipAccelerationHandler()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipAccelerationHandler>()
                .AsSingle();
        }

        private void BindSpaceshipInertiaHandler()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipInertiaHandler>()
                .AsSingle();
        }

        private void BindSpaceshipMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipMovementController>()
                .AsSingle();
        }

        private void BindSpaceshipSpawner(SpaceshipComponent spaceshipPrefab)
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .WithArguments(spaceshipPrefab);
        }
        
    }
}