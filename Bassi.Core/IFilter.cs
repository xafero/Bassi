namespace Bassi.Core
{
    public interface IFilter
    {
        string Name { get; }

        bool IsValid(string file);
    }
}