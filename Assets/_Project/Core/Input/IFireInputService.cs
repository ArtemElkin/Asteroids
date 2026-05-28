using System;
using UnityEngine;


namespace _Project.Core.Input
{
    public interface IFireInputService
    {
        event Action<bool> FireStateChanged;
        Vector2 GetScreenPointerPosition();
    }
}