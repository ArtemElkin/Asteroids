using _Project.Core.Math;
using UnityEngine;
using Zenject;


namespace _Project.Core.Tools
{
    public class FactoryWithPool<T> : IInitializable where T : MonoBehaviour
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
        }

        public void Initialize()
        {
            _pool = new CustomPool<T>(_instantiator, _prefab, defaultParentTransform:_defaultParentTransform);
        }

        public T Create(CustomVector2 localPosition, Transform parentTransform = null)
        {
            var obj = _pool.Get(parentTransform);
            obj.transform.localPosition = (Vector2)localPosition;
            return obj;
        }

        public void Release(T obj)
        {
            _pool.Release(obj);
        }
    }
}