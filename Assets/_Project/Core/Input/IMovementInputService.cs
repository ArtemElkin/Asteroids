using System;


namespace _Project.Core.Input
{
    public interface IMovementInputService
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
    }
}