using System.Collections.Generic;

namespace _Project.Core.GameLifecycle
{
    public class WorldResetService : IWorldResetService
    {
        private readonly List<IWorldResettable> _resettables;


        public WorldResetService(List<IWorldResettable> resettables)
        {
            _resettables = resettables;
            
        }
        public void ResetWorld()
        {
            foreach (var resettable in _resettables)
            {
                resettable.Reset();
            }
        }
    }
}