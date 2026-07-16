using System;
using System.Collections.Generic;
using _Project.Core.Math;
using _Project.Core.Render;

namespace _Project.Features.Common.ScreenWrapClone
{
    public interface IScreenWrapCloneSet : IDisposable
    {
        IReadOnlyCollection<IDrawable> ClonesDrawables { get; }
        void CreateClones();
        void UpdateClones();
    }
}