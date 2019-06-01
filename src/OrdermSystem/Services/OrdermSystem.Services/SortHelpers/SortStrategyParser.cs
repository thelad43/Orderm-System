namespace OrdermSystem.Services.SortHelpers
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class SortStrategyParser : ISortStrategyParser
    {
        private readonly IServiceProvider serviceProvider;

        public SortStrategyParser(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ISortStrategy<T> Parse<T>(string sort)
        {
            const string SortStrategy = "SortStrategy";

            var assembly = Assembly.GetExecutingAssembly();

            var sortTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ISortStrategy<T>)))
                .ToArray();

            var sortType = sortTypes
                .SingleOrDefault(t => t.Name.ToLower() == $"{sort.ToLower()}{SortStrategy.ToLower()}");

            if (sortType == null)
            {
                return null;
            }

            var sortStrategy = this.InjectServices<T>(sortType);

            return sortStrategy;
        }

        private ISortStrategy<T> InjectServices<T>(Type type)
        {
            var constructor = type.GetConstructors().First();

            var constructorParameters = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(this.serviceProvider.GetService)
                .ToArray();

            var sortStrategy = (ISortStrategy<T>)constructor.Invoke(services);

            return sortStrategy;
        }
    }
}