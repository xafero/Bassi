using System;

namespace Bassi.Core
{
    public class LambdaFilter : AbstractFilter
    {
        public override string Name { get; }
        private Func<IHandle, bool> func;

        public LambdaFilter(string name, Func<IHandle, bool> func)
        {
            Name = name;
            this.func = func;
        }

        protected override bool IsValid(IHandle file) => func(file);
    }
}