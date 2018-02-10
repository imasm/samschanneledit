using System;

namespace SamsChannelEditor.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class MaxLengthAttribute: Attribute
    {
        public virtual int Value { get; }

        public MaxLengthAttribute(int value)
        {
            this.Value = value;
        }
    }
}
