using AutoMapper;
using Cobros.API.Core.Mapper;
using Cobros.API.DataAccess;
using Cobros.API.Repositories;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobros.Test
{
    public class TestHelpers
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TestHelpers()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "testDb");

            _applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(_applicationDbContext);
        }

        public IMapper GetAutoMapperInstance()
        {
            var mapperConfiguration = new MapperConfiguration(options => options.AddProfile(new AutoMapperProfile()));
            return new Mapper(mapperConfiguration);
        }
    }
}
