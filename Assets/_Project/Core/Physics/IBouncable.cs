using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IBouncable
    {
        void Bounce(Vector2 normal);
    }
}