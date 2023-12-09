using Microsoft.EntityFrameworkCore;
using Nns.Orders.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.UnitTests
{
    public class OrderDbTestBase
    {
        protected readonly OrderDbContext _db;

        public OrderDbTestBase()

        {
            var uniqueId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                          .UseInMemoryDatabase(databaseName: uniqueId)
                          .Options;

            _db = new OrderDbContext(options);
            _db.Database.EnsureCreated();

            PrepareData();
        }

        protected virtual void PrepareData()
        {}



        public void Dispose()

        {

            _db.Database.EnsureDeleted();

            _db.Dispose();

        }
    }
}
