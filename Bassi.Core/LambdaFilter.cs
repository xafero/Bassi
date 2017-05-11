using System;

namespace Bassi.Core
{
    public class LambdaFilter : AbstractFilter
    {
        public override string Name { get; }
        private Func<IHandle, bool> func;
        private Func<string, IFilter> refer;

        public LambdaFilter(string name, Func<IHandle, bool> func, Func<string, IFilter> refer)
        {
            Name = name;
            this.func = func;
            this.refer = refer;
        }

        protected override bool IsValid(IHandle file) => func(file);

        protected override IFilter GetRef(string name) => refer(name);
    }
}