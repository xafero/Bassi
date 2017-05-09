using System;

namespace Bassi.Core
{
    public class LambdaFilter : AbstractFilter
    {
        public override string Name { get; }
        private Func<Handle, bool> func;

        public LambdaFilter(string name, Func<Handle, bool> func)
        {
            Name = name;
            this.func = func;
        }

        protected override bool IsValid(Handle file) => func(file);
    }
}