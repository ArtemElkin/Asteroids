using System.Collections.Generic;
using _Project.Core.Math;
using _Project.Core.Render;

namespace _Project.Features.Common.ScreenWrapClone
{
    public class NullScreenWrapCloneSet : IScreenWrapCloneSet
    {
        private readonly List<IDrawable> _clonesDrawables = new();
        public IReadOnlyCollection<IDrawable> ClonesDrawables => _clonesDrawables;
        public void CreateClones() { }

        public void UpdateClones() { }
        
        public void Dispose() { }
    }
}