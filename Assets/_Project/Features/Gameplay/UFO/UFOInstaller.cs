using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.UFO
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
                .Bind<Infrastructure.Factories.IFactory<UFOSpawnData, UFOFacade>>()
                .To<UFOFactory>()
                .AsSingle()
                .WithArguments(ufoPrefab, parentTransform)
                .NonLazy();
        }

        private void BindUFOSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<UFOSpawner>()
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