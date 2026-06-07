using _Project.Core.Tools;
using _Project.Features.Common.Clone;
using _Project.Features.Spaceship;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private MovableView _spaceshipPrefab;
        [SerializeField] private Transform _spaceshipParentTransform;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipStorage();
            BindSpaceshipFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipSpawner();
            BindSpaceshipDespawner();
            
            BindSpaceshipCloneStorage();
            BindSpaceshipCloneFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipCloneSpawner();
            BindSpaceshipCloneDespawner();
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
                .Bind<Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>>()
                .To<SpaceshipFactory>()
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

        private void BindSpaceshipDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipDespawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipCloneStorage()
        {
            Container
                .Bind<CloneStorage<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipCloneFactory(
            MovableView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<CloneSpawnData, CloneFacade<SpaceshipFacade>>>()
                .To<CloneFactory<SpaceshipFacade>>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }

        private void BindSpaceshipCloneSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<CloneSpawner<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipCloneDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<CloneDespawner<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }
    }
}