using System;

namespace Plugins.MVVM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SetterAttribute : MemberAttribute
    {
        public SetterAttribute(object id) : base(id)
        {
        }
    }
}