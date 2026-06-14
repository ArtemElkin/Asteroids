using UnityEngine;

namespace _Project.Infrastructure.Render
{
    public class SpaceshipView : MovableView
    {
        [SerializeField] private MuzzleView _muzzleView;

        public MuzzleView GetMuzzleView() => _muzzleView;
    }
}