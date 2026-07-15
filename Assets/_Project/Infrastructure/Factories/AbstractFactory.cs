using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public abstract class AbstractFactory<TSpawnData, TEntity, TView> 
        : Core.Factories.IFactory<TSpawnData, TEntity> where TView : MonoBehaviour
    {
        protected readonly CustomPool<TView> _pool;
        private readonly IInstantiator _instantiator;


        protected AbstractFactory(
            IInstantiator instantiator,
            TView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _pool = new CustomPool<TView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }

        public abstract TEntity Create(TSpawnData data);

        protected T CreateComponent<T>(params object[] extraArgs) => _instantiator.Instantiate<T>(extraArgs);
    }
}