using System;


namespace _Project.Core.Input
{
    public interface IFireInputService
    {
        event Action<bool> FireStateChanged;
    }
}