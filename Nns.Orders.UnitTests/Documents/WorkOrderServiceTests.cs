using FluentAssertions;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Logic;

namespace Nns.Orders.UnitTests;

public class WorkOrderServiceTests : OrderDbTestBase
{
    EquipmentState _equipmentState;
    WorkCycleState _workState;
    WorkOrderService _service;

    public WorkOrderServiceTests()
    {
        _equipmentState = new EquipmentState(_db);
        _workState = new WorkCycleState(_db);
        _service = new WorkOrderService(_db, _workState, _equipmentState);
    }

    protected override void PrepareData()
    {
        var applications = new[]
          {
            //01.01 
            new EquipmentApplication
                {StartDate = new DateTime(2020, 1, 1), WorkTypeId = 1, EquipmentTypeId = 1, IsActive = true},
            new EquipmentApplication
                {StartDate = new DateTime(2020, 1, 1), WorkTypeId = 2, EquipmentTypeId = 2, IsActive = true},

            //01.02
            new EquipmentApplication
                {StartDate = new DateTime(2020, 1, 2), WorkTypeId = 2, EquipmentTypeId = 2, IsActive = false},            

            //01.02
            new EquipmentApplication
                {StartDate = new DateTime(2020, 1, 3), WorkTypeId = 3, EquipmentTypeId = 3, IsActive = true}
        };
        _db.EquipmentApplications.AddRange(applications);

        var workCycles = new[]
        {
            new WorkCycle {Id=1,StartDate = new DateTime(2020, 1, 1), WorkTypeId = 1, OrderNumber = 1, IsActive = true},
            new WorkCycle {Id=2,StartDate = new DateTime(2020, 1, 1), WorkTypeId = 2, OrderNumber = 2, IsActive = true},
            new WorkCycle {Id=3,StartDate = new DateTime(2020, 1, 1), WorkTypeId = 3, OrderNumber = 3, IsActive = true},

            // на 02.01 отменили 
            new WorkCycle {Id=4,StartDate = new DateTime(2020, 1, 2), WorkTypeId = 2, OrderNumber = 2, IsActive = false},

            new WorkCycle {Id=5,StartDate = new DateTime(2020, 1, 2), WorkTypeId = 4, OrderNumber = 4, IsActive = false},


        };

        _db.WorkCycles.AddRange(workCycles);

        _db.SaveChanges();
    }

    [Fact]
    public async Task AddWorkOrders_ShouldBeCreated()
    {            

        var model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 1), ExcavationId = 1, WorkTypeId = 1, EquipmentTypeId = 1,OrderNumber = 3, Value = 10
        };

        await _service.Add(model);

        _db.WorkOrders.Should().ContainSingle();
        

        model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 1),ExcavationId = 1,WorkTypeId = 2,EquipmentTypeId = 2, OrderNumber = 7,Value = 10
        };

        await _service.Add(model);

        _db.WorkOrders.Should().HaveCount(c => c == 2);       
    }

    [Fact]
    public async Task AddWorkOrder_CantApply_ShouldFail()
    {

        var model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 1),
            ExcavationId = 1,
            WorkTypeId = 2,
            EquipmentTypeId = 2, // применяемость есть на 01.01
            OrderNumber = 3,
            Value = 10
        };

        await _service.Add(model);        

        model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 2),
            ExcavationId = 1,
            WorkTypeId = 2,
            EquipmentTypeId = 2, // применяемости нет на 02.01
            OrderNumber = 3,
            Value = 10
        };

        Func<Task<long>> funcTask = (async () => await _service.Add(model));

        await funcTask.Should().ThrowAsync<AppException>().WithMessage("Применяемость не активна WorkTypeId=2 EquipmentTypeId=2");
    }

    [Fact]
    public async Task AddWorkOrders_MeetOrder_ShouldSucceed()
    {

        var model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 3),
            ExcavationId = 1,
            WorkTypeId = 1,
            EquipmentTypeId = 1, 
            OrderNumber = 1,
            Value = 10
        };

        await _service.Add(model);


        model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 3),
            ExcavationId = 1,
            WorkTypeId = 3,
            EquipmentTypeId = 3, 
            OrderNumber = 3,
            Value = 10
        };               

        await _service.Add(model);

        _db.WorkOrders.Should().HaveCount(c => c == 2);
    }

    public async Task AddWorkOrders_DoenntMeetOrder_ShouldFail()
    {

        var model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 3),
            ExcavationId = 1,
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            OrderNumber = 5,
            Value = 10
        };

        await _service.Add(model);


        model = new WorkOrder
        {
            StartDate = new DateTime(2020, 1, 3),
            ExcavationId = 1,
            WorkTypeId = 3,
            EquipmentTypeId = 3,
            OrderNumber = 3,
            Value = 10
        };

        Func<Task<long>> funcTask = (async () => await _service.Add(model));

        await funcTask.Should().ThrowAsync<AppException>().WithMessage("Порядок видов работ в плане не соответствует порядку в активном производственном цикле");
    }
}