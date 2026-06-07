using System;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public interface IFacade : IDisposable
    {
        IDrawable GetDrawable();
        IReadOnlyPositionable GetPositionable();
        IReadOnlyRotationable GetRotationable();
    }
}