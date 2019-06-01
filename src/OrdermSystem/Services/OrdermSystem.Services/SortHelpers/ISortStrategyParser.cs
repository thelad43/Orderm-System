namespace OrdermSystem.Services.SortHelpers
{
    public interface ISortStrategyParser
    {
        ISortStrategy<T> Parse<T>(string sort);
    }
}