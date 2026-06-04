using System;
using _Project.Core.Math;

namespace _Project.Core.Input
{
    public interface IFireInputService
    {
        event Action<bool> FireStateChanged;
        bool FireState { get; }
        Vector2 GetScreenPointerPosition();
    }
}