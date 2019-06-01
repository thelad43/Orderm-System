namespace OrdermSystem.Services.SortHelpers
{
    using System.Linq;

    public interface ISortStrategy<T>
    {
        IQueryable<T> Sort(IQueryable<T> elements);
    }
}