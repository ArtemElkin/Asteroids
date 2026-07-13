using System.Collections;
using System.Collections.Generic;

namespace _Project.Core.Tools
{
    public class Storage<T> : IEnumerable<T> where T : class
    {
        private readonly List<T> _storage = new ();
        
        public void Add(T obj) => _storage.Add(obj);
        public void Remove(T obj) => _storage.Remove(obj);
        public int Count => _storage.Count;

        public bool TryGetFirst(out T obj)
        {
            if (!IsEmpty)
            {
                obj = _storage[0];
                return true;
            }
            obj = null;
            return false;
        }

        public IReadOnlyCollection<T> GetCopy() => new List<T>(_storage);
        public bool IsEmpty => _storage.Count == 0;
        public void Clear() => _storage.Clear();
        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}