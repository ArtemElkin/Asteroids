using UnityEngine;


namespace _Project.Features.Gameplay
{
    public interface IWarpable
    {
        void Warp(Vector3 position);
        Vector2 GetLastPosition();
    }
}