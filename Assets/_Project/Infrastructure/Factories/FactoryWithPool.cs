using _Project.Infrastructure.Tools;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Factories
{
    public class FactoryWithPool<T> where T : MonoBehaviour
    {
        private CustomPool<T> _pool;
        private readonly T _prefab;
        private readonly IInstantiator _instantiator;
        private readonly Transform _defaultParentTransform;
        
        
        public FactoryWithPool(IInstantiator instantiator, T prefab, Transform defaultParentTransform = null)
        {
            _instantiator = instantiator;
            _prefab =  prefab;
            _defaultParentTransform = defaultParentTransform;
            
            _pool = new CustomPool<T>(_instantiator, _prefab, defaultParentTransform:_defaultParentTransform);
        }

        public T Create(Vector2 localPosition, Transform parentTransform = null)
        {
            var obj = _pool.Get(parentTransform);
            obj.transform.localPosition = localPosition.ToUnity();
            return obj;
        }

        public void Release(T obj)
        {
            _pool.Release(obj);
        }
    }
}