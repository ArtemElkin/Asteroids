using System;
using JetBrains.Annotations;

namespace Plugins.MVVM.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DataAttribute : MemberAttribute
    {
        public DataAttribute(object id) : base(id)
        {
        }
    }
}
