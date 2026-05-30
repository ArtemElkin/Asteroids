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
        [SerializeField] private Transform _spaceshipParentTransform;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipStorage();
            BindSpaceshipMovementController();
            BindSpaceshipRotationController();
            BindSpaceshipSpawner(_spaceshipPrefab, _spaceshipClonePrefab, _spaceshipParentTransform);
        }

        private void BindSpaceshipStorage()
        {
            Container
                .Bind<Storage<SpaceshipComponent>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipMovementController()
        {
            Container
                .Bind<SpaceshipMovementController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipRotationController()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipRotationController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            SpaceshipCloneComponent spaceshipClonePrefab,
            Transform spaceshipParentTransform)
        {
            Container.DeclareSignal<SpawnedSignal<SpaceshipComponent>>();

            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipClonePrefab, spaceshipParentTransform)
                .NonLazy();
        }
        
    }
}