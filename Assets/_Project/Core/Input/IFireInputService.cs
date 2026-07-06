using _Project.Core.Math;

namespace _Project.Core.Input
{
    public interface IFireInputService
    {
        bool FireState(int buttonId);
        Vector2 GetAimDirection(Vector2 from);
    }
}