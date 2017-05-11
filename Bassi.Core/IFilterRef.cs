namespace Bassi.Core
{
    public interface IFilterRef
    {
        bool this[string name] { get; }
    }
}