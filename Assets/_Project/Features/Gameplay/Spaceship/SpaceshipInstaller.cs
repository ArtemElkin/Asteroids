using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private SpaceshipComponent _spaceshipPrefab;
        [SerializeField] private SpaceshipCloneComponent _spaceshipClonePrefab;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipAccelerationApplier();
            BindSpaceshipInertiaApplier();
            BindSpaceshipMovementController();
            BindSpaceshipRotationController();
            BindSpaceshipSpawner(_spaceshipPrefab, _spaceshipClonePrefab);
        }

        private void BindSpaceshipAccelerationApplier()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipAccelerationApplier>()
                .AsSingle();
        }

        private void BindSpaceshipInertiaApplier()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipInertiaApplier>()
                .AsSingle();
        }

        private void BindSpaceshipMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipMovementController>()
                .AsSingle();
        }

        private void BindSpaceshipRotationController()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipRotationController>()
                .AsSingle();
        }

        private void BindSpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            SpaceshipCloneComponent spaceshipClonePrefab)
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipClonePrefab);
        }
        
    }
}