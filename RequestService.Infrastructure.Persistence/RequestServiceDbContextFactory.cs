﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RequestService.Infrastructure.Persistence.SharedKernel;

namespace RequestService.Infrastructure.Persistence
{
    public class RequestServiceDbContextFactory : DesignTimeDbContextFactoryBase<RequestServiceDbContext>
    {
        protected override RequestServiceDbContext CreateNewInstance(DbContextOptions<RequestServiceDbContext> options)
        {
            return new RequestServiceDbContext(options);
        }
    }
}
