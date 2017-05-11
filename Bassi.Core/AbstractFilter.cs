namespace Bassi.Core
{
    public abstract class AbstractFilter : IFilter
    {
        public abstract string Name { get; }

        protected abstract bool IsValid(IHandle file);

        protected abstract IFilter GetRef(string name);

        public bool IsValid(string file) => IsValid(new Handle(file, GetRef));
    }
}