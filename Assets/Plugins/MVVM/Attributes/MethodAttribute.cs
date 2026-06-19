using System;
using JetBrains.Annotations;

namespace Plugins.MVVM.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MethodAttribute : MemberAttribute
    {
        public MethodAttribute(object id) : base(id)
        {
        }
    }
}