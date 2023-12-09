using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Logic;

namespace Nns.Orders.UnitTests
{
    public class WorkCycleTests : OrderDbTestBase
    {
        WorkCycleState _workCycleState;
        WorkCycleService _service;

            public WorkCycleTests()
        {
            _workCycleState = new WorkCycleState(_db);
             _service = new WorkCycleService(_db, _workCycleState);
        }

        [Fact]
        public async Task AddWorkCycle_SouldBeCreated()
        {          

            var model = new WorkCycle
            {
                StartDate = new DateTime(2020, 1, 1),
                WorkTypeId = 1,
                OrderNumber= 1,                
                IsActive = true
            };

            await _service.Add(model);

           Assert.Equal(1, _db.WorkCycles.Count());
        }
       

        [Fact]
        public async Task AddWcWhenDouble_ShouldFail()
        {          

            _db.WorkCycles.Add(new WorkCycle
            {
                StartDate = new DateTime(2020, 1, 1),
                WorkTypeId = 1,
                OrderNumber =1,
                IsActive = true,
            });

            await _db.SaveChangesAsync();

            var model = new WorkCycle
            {
                StartDate = new DateTime(2020, 1, 2),
                WorkTypeId = 1,
                OrderNumber = 1,
                IsActive = true,
            };

            await  Assert.ThrowsAsync<AppException>(async () => await _service.Add(model));

        }



        [Fact]
        public async Task AddWcAnotherDimentions_ShouldPass()
        {
                         

                _db.WorkCycles.Add(new WorkCycle
                {
                    StartDate = new DateTime(2020, 1, 1),
                    WorkTypeId = 1,
                    OrderNumber = 1,
                    IsActive = true,
                });

                await _db.SaveChangesAsync();

                var model = new WorkCycle
                {
                    StartDate = new DateTime(2020, 1, 2),
                    WorkTypeId = 2,
                    OrderNumber = 2,
                    IsActive = true,
                };

                await _service.Add(model);

                Assert.Equal(2, _db.WorkCycles.Count());

            
        }

        [Fact]
        public async Task AddWorkCycle_ShouldPassIfCorrect()
        {              

                _db.WorkCycles.AddRange( new WorkCycle {
                StartDate = new DateTime(2020, 1, 1),
                WorkTypeId = 1,
                OrderNumber = 1,
                IsActive = true,
            },
                new WorkCycle
                {
                    StartDate = new DateTime(2020, 1, 2),
                    WorkTypeId = 2,
                    OrderNumber = 2,
                    IsActive = true,
                }
                );

            await _db.SaveChangesAsync();

            var model = new WorkCycle
            {
                StartDate = new DateTime(2020, 1, 3),
                WorkTypeId = 3,
                OrderNumber = 3,
                IsActive = true,
            };

            await _service.Add(model);

            Assert.Equal(3, _db.WorkCycles.Count());

        }
    }
}