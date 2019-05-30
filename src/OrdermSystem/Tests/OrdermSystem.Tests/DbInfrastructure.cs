namespace OrdermSystem.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using OrdermSystem.Data;

    public class DbInfrastructure
    {
        public static ApplicationDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(dbOptions);
        }
    }
}