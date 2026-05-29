using System;
using _Project.Core.Math;


namespace _Project.Core.Input
{
    public interface IFireInputService
    {
        event Action<bool> FireStateChanged;
        CustomVector2 GetScreenPointerPosition();
    }
}