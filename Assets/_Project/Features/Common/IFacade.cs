using System;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public interface IFacade : IDisposable
    {
        MovementModel MovementModel { get; }
        IDrawable GetDrawable();
    }
}