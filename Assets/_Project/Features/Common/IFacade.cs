using System;
using _Project.Core.Render;

namespace _Project.Features.Common
{
    public interface IFacade : IDisposable
    {
        IDrawable Drawable { get; }
    }
}