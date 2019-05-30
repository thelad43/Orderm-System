namespace OrdermSystem.Tests
{
    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Services;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                AutoMapperConfig.RegisterMappings(typeof(IService).Assembly);
                testsInitialized = true;
            }
        }
    }
}