namespace _Project.Core.Physics
{
    public interface IStunnable : IReadOnlyStunState
    {
        void SetStunned(bool isStunned);
    }
}