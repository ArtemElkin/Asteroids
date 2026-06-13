using System;
using _Project.Core.Render;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public interface IFacade : IDisposable
    {
        IDrawable Drawable { get; }
    }
}