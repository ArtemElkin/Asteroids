using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.UFO;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class UFOInstaller : MonoInstaller
    {
        [SerializeField] private MovableView _ufoPrefab;
        [SerializeField] private Transform _ufoParentTransform;
        
        
        public override void InstallBindings()
        {
            BindUFOStorage();
            BindUFOFactory(_ufoPrefab, _ufoParentTransform);
            BindUFOSpawner();
            BindUFODespawner();
        }

        private void BindUFOStorage()
        {
            Container
                .Bind<Storage<UFOFacade>>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindUFOFactory(MovableView ufoPrefab, Transform parentTransform)
        {
            Container
                .Bind(
                    typeof(Core.Factories.IFactory<UFOSpawnData, UFOFacade>),
                    typeof(IReleaser<UFOFacade>))
                .To<UFOFactory>()
                .AsSingle()
                .WithArguments(ufoPrefab, parentTransform)
                .NonLazy();
        }

        private void BindUFOSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<UFOSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUFODespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<UFOFacade>>()
                .AsSingle()
                .NonLazy();
        }
    }
}