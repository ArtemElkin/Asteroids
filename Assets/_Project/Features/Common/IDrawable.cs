using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public interface IDrawable
    {
        void Setup(MovementModel model);
        void Draw();
        void Reset();
    }
}