using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Puma.Prey.Rabbit.Context;
using Xunit;

namespace Coyote.Tests.Core
{
    public static class DbTestData
    {
        //     public static PumaDbContext CreateInMemoryDbContextWithData()
        //     {
        //         var dbContext = new PumaDbContext(new DbContextOptionsBuilder<PumaDbContext>()
        //             .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //             .Options);

        //         return InitializeExistingDbContextWithData(dbContext);
        //     }

        //     public static PumaDbContext InitializeExistingDbContextWithData(PumaDbContext dbContext)
        //     {
        //         PumaProductInitialization.Initialize(dbContext);
        //         PumaRolesInitialization.Initialize(dbContext);
        //         return dbContext;
        //     }
    }
}
