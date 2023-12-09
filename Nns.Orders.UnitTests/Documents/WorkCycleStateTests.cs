using FluentAssertions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Domain.Entities;
using Nns.Orders.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.UnitTests.Documents
{
    public class WorkCycleStateTests : OrderDbTestBase
    {

        protected override void PrepareData()
        {
            var date_01 = new DateTime(2020, 1, 1);
            var date_02 = new DateTime(2020, 1, 2);
            var date_03 = new DateTime(2020, 1, 3);
            var date_04 = new DateTime(2020, 1, 4);

            var workCycles = new[] {
                new WorkCycle{Id=1,StartDate = date_01,WorkTypeId = 1,OrderNumber = 1,IsActive = true},
                new WorkCycle{Id=2,StartDate = date_01,WorkTypeId = 2,OrderNumber = 2,IsActive = true},
                new WorkCycle{Id=3,StartDate = date_01,WorkTypeId = 3,OrderNumber = 3,IsActive = true},

                // 02 отменили последнюю
                new WorkCycle{Id=4,StartDate = date_02,WorkTypeId = 3,OrderNumber = 3,IsActive = false},

                // 03 добавили отмененную
                new WorkCycle{Id=5,StartDate = date_03,WorkTypeId = 3,OrderNumber = 3,IsActive = true},

                // 04 добавили новую, отменили первую
                new WorkCycle{Id=6,StartDate = date_04,WorkTypeId = 4,OrderNumber = 4,IsActive = true},
                new WorkCycle{Id=7,StartDate = date_04,WorkTypeId = 1,OrderNumber = 1,IsActive = false},
            };

            _db.WorkCycles.AddRange(workCycles);

             _db.SaveChanges();
        }
        [Fact]
        public async Task WorkCycleState_SouldBeCreated()
        {
            var date_01 = new DateTime(2020, 1, 1);
            var date_02 = new DateTime(2020, 1, 2);
            var date_03 = new DateTime(2020, 1, 3);
            var date_04 = new DateTime(2020, 1, 4);

            var stateService = new WorkCycleState(_db);
                  

            var state = await stateService.GetWorkCycles(date_01, _=>true);

            state.Should().HaveCount(3);
            state.Select(x => (int)x.Id).Should().BeSubsetOf(new[] { 1, 2, 3 });

            state = await stateService.GetWorkCycles(date_02, _ => true);

            state.Should().HaveCount(3);            
            state.Select(x => (int)x.Id).Should().BeSubsetOf(new[] { 1, 2, 4 });


            state = await stateService.GetWorkCycles(date_03, _ => true);

            state.Should().HaveCount(3);
            state.Select(x => (int)x.Id).Should().BeSubsetOf(new[] { 1, 2, 5 });

            state = await stateService.GetWorkCycles(date_04, _ => true);

            state.Should().HaveCount(4);
            state.Select(x => (int)x.Id).Should().BeSubsetOf(new[] { 2, 5, 6, 7 });
        }
    }
}
