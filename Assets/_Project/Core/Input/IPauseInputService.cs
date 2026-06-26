using System;

namespace _Project.Core.Input
{
    public interface IPauseInputService
    {
        event Action OnPause;
    }
}