using System;

namespace _Project.Features.Common
{
    public interface IFacade : IDisposable
    {
        public IDrawable GetDrawable();
    }
}