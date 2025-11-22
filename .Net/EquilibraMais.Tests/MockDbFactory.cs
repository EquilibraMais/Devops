namespace EquilibraMais.Tests;

using EquilibraMais.DbConfig;
using Microsoft.EntityFrameworkCore;
using System;

public static class MockDbFactory
{
    public static EquilibraMaisDbContext Create()
    {
        var options = new DbContextOptionsBuilder<EquilibraMaisDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new EquilibraMaisDbContext(options);
    }
}
