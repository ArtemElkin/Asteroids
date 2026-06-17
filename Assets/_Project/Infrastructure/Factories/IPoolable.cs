using UnityEngine;

namespace _Project.Infrastructure.Factories
{
    public interface IPoolable
    {
        MonoBehaviour GetPoolableComponent();
    }
}