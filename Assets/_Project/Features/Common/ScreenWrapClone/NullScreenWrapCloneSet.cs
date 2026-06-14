using System.Collections.Generic;
using _Project.Core.Render;

namespace _Project.Features.Common.ScreenWrapClone
{
    public class NullScreenWrapCloneSet : IScreenWrapCloneSet
    {
        private List<IDrawable> _clonesDrawables = new ();
        public IReadOnlyCollection<IDrawable> ClonesDrawables => _clonesDrawables;
        public void UpdateClones() { }
        
        public void Dispose() { }
    }
}