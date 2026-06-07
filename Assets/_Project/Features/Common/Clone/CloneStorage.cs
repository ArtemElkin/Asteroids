using System.Collections.Generic;

namespace _Project.Features.Common.Clone
{
    public class CloneStorage<TOriginFacade> where TOriginFacade : IFacade 
    {
        private readonly Dictionary<TOriginFacade, List<CloneFacade<TOriginFacade>>> _storage = new ();
        
        private void AddOrigin(TOriginFacade originFacade) => _storage[originFacade] = new List<CloneFacade<TOriginFacade>>();

        public void AddClone(TOriginFacade originFacade, CloneFacade<TOriginFacade> cloneFacade)
        {
            if (!_storage.ContainsKey(originFacade))
            {
                AddOrigin(originFacade);
            }
            _storage[originFacade].Add(cloneFacade);
        }

        public void RemoveClone(TOriginFacade originFacade, CloneFacade<TOriginFacade> cloneFacade)
        {
            _storage[originFacade]?.Remove(cloneFacade);
        }
        public void RemoveClones(TOriginFacade originFacade) => _storage[originFacade].Clear();

        public IReadOnlyCollection<CloneFacade<TOriginFacade>> GetAllClones(TOriginFacade originFacade)
        {
            if (_storage.ContainsKey(originFacade))
            {
                return new List<CloneFacade<TOriginFacade>>(_storage[originFacade]);
            }
            return  new List<CloneFacade<TOriginFacade>>();
        }

        public bool IsEmpty => _storage.Count == 0;
        public void Clear() => _storage.Clear();
    }
}