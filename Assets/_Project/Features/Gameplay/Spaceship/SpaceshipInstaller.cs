using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private MovableView _spaceshipPrefab;
        [SerializeField] private Transform _spaceshipParentTransform;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipStorage();
            BindSpaceshipFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipCloneFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipSpawner();
        }

        private void BindSpaceshipStorage()
        {
            Container
                .Bind<Storage<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipFactory(
            MovableView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind<Infrastructure.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>>()
                .To<SpaceshipFactory>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }

        private void BindSpaceshipCloneFactory(
            MovableView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind<Infrastructure.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade>>()
                .To<SpaceshipCloneFactory>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }

        private void BindSpaceshipSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .NonLazy();
        }
        
    }
}