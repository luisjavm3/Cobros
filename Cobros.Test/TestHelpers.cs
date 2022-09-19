using AutoMapper;
using Cobros.API.Core.Mapper;
using Cobros.API.DataAccess;
using Cobros.API.Repositories;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Cobros.Test
{
    public class TestHelpers
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TestHelpers()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "testDb");
            optionsBuilder.ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            _applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(_applicationDbContext);
        }

        public IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(options => options.AddProfile(new AutoMapperProfile()));
            return new Mapper(mapperConfiguration);
        }

        public IConfiguration GetConfiguration()
        {
            var appsettings = @"{
                  ""Logging"": {
                    ""LogLevel"": {
                      ""Default"": ""Information"",
                      ""Microsoft.AspNetCore"": ""Warning""
                    }
                  },
                  ""ConnectionStrings"": {
                    ""DefaultConnection"": ""Server=DESKTOP-AHB8AM9\\SQLEXPRESS;Database=cobros;Trusted_Connection=true;""
                  },
                  ""JwtSettings"": {
                    ""Key"": ""my_long_enough_jwt_secrect_123"",
                    ""LifetimeMinutes"": 30
                  },
                  ""AllowedHosts"": ""*""
                }";

            return new ConfigurationBuilder()
                        .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appsettings)))
                        .Build();
        }
    }
}
