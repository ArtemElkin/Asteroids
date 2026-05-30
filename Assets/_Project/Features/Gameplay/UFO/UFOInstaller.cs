using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOInstaller : MonoInstaller
    {
        [SerializeField] private UFOComponent _ufoPrefab;
        [SerializeField] private Transform _ufoParentTransform;
        
        
        public override void InstallBindings()
        {
            BindUFOStorage();
            BindUFOMovementController();
            BindUFORotationController();
            BindUFOTargetFollower();
            BindUFOFactory(_ufoPrefab, _ufoParentTransform);
            BindUFOBuilder();
            BindUFOSpawner();
            BindUFOSpawnTimer();
            BindUFODespawner();
        }

        private void BindUFOStorage()
        {
            Container
                .Bind<Storage<UFOComponent>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUFOMovementController()
        {
            Container
                .Bind<UFOMovementController>()
                .AsTransient();
        }

        private void BindUFORotationController()
        {
            Container
                .Bind<UFORotationController>()
                .AsTransient();
        }

        private void BindUFOTargetFollower()
        {
            Container
                .Bind<UFOTargetFollower>()
                .AsTransient();
        }

        private void BindUFOFactory(UFOComponent ufoPrefab, Transform parentTransform)
        {
            Container
                .Bind<FactoryWithPool<UFOComponent>>()
                .AsSingle()
                .WithArguments(ufoPrefab, parentTransform)
                .NonLazy();
        }

        private void BindUFOBuilder()
        {
            Container
                .BindInterfacesAndSelfTo<UFOBuilder>()
                .AsSingle();
        }

        private void BindUFOSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<UFOSpawner>()
                .AsSingle();
        }

        private void BindUFOSpawnTimer()
        {
            Container
                .BindInterfacesAndSelfTo<SpawnTimer<UFOComponent>>()
                .AsSingle();
        }

        private void BindUFODespawner()
        {
            Container
                .BindInterfacesAndSelfTo<UFODespawner>()
                .AsSingle();
        }
    }
}