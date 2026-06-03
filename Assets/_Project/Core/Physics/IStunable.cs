namespace _Project.Core.Physics
{
    public interface IStunable : IReadOnlyStunable
    {
        void SetStunned(bool isStunned);
    }

    public interface IReadOnlyStunable
    {
        bool IsStunned { get; }
    }
}