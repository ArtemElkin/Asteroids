using JetBrains.Annotations;

namespace Plugins.MVVM.Attributes
{
    [MeansImplicitUse]
    public abstract class MemberAttribute : System.Attribute
    {
        internal readonly object id;

        protected MemberAttribute(object id)
        {
            this.id = id;
        }
    }
}