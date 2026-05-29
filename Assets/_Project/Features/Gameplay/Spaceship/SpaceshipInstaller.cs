using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
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
            BindSpaceshipStorage();
            BindSpaceshipAccelerationApplier();
            BindSpaceshipInertiaApplier();
            BindSpaceshipMovementController();
            BindSpaceshipRotationController();
            BindSpaceshipSpawner(_spaceshipPrefab, _spaceshipClonePrefab);
        }

        private void BindSpaceshipStorage()
        {
            Container
                .BindInterfacesAndSelfTo<Storage<SpaceshipComponent>>()
                .AsSingle();
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
            Container.DeclareSignal<SpawnedSignal<SpaceshipComponent>>();
            
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipClonePrefab);
        }
        
    }
}