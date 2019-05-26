namespace OrdermSystem.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public class Configuration
    {
        public void Configure<TClass, TConfig>(ModelBuilder builder)
            where TClass : class
            where TConfig : IEntityTypeConfiguration<TClass>
        {
            var configuration = Activator.CreateInstance<TConfig>();
            builder.ApplyConfiguration(configuration);
        }
    }
}