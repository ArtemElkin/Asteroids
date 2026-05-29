using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
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
            BindUFOFactory(_ufoPrefab, _ufoParentTransform);
            BindUFOSpawner();
            BindUFOSpawnTimer();
            BindUFODespawner();
        }

        private void BindUFOStorage()
        {
            Container
                .BindInterfacesAndSelfTo<Storage<UFOComponent>>()
                .AsSingle();
        }

        private void BindUFOMovementController()
        {
            Container
                .Bind<UFOMovementController>()
                .AsTransient();
        }

        private void BindUFOFactory(UFOComponent ufoPrefab, Transform parentTransform)
        {
            Container
                .BindInterfacesAndSelfTo<FactoryWithPool<UFOComponent>>()
                .AsSingle()
                .WithArguments(_ufoPrefab, parentTransform);
        }

        private void BindUFOSpawner()
        {
            Container.DeclareSignal<SpawnedSignal<UFOComponent>>();
            
            Container
                .BindInterfacesAndSelfTo<UFOSpawner>()
                .AsSingle();
        }

        private void BindUFOSpawnTimer()
        {
            Container.DeclareSignal<SpawnRequestedSignal<UFOComponent>>();

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