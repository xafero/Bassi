using System;

namespace Bassi.Core
{
    internal class LambdaRefer : IFilterRef
    {
        private string file;
        private Func<string, IFilter> func;

        public LambdaRefer(string file, Func<string, IFilter> func)
        {
            this.file = file;
            this.func = func;
        }

        public bool this[string name] => func(name).IsValid(file);
    }
}