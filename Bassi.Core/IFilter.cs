namespace Bassi.Core
{
    public interface IFilter
    {
        bool IsValid(string file);
    }
}