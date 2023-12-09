using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Logic;

namespace Nns.Orders.UnitTests;

public class ApplicationServiceTests : OrderDbTestBase
{
    EquipmentApplicationService _service;

    EquipmentState _state;
    public ApplicationServiceTests()
    {
        _state = new EquipmentState(_db);
        _service = new EquipmentApplicationService(_db, _state);
    }

    [Fact]
    
    public async Task AddEquipmentApplication_ShouldPass()
    {        
    
        var model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 1),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive = true
        };        

        await _service.Add(model);

        Assert.Equal(1, _db.EquipmentApplications.Count());

        model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 1),
            WorkTypeId = 2,
            EquipmentTypeId = 2,
            IsActive = true
        };
        await _service.Add(model);

        Assert.Equal(2, _db.EquipmentApplications.Count());
    }
   

    [Fact]
    public async Task AddEquipmentApplication_SouldFailWhenRetro()
    {        

        _db.EquipmentApplications.Add(new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 2),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive = true
        });

        await _db.SaveChangesAsync();

        var model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 1),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive = true
        };

        await  Assert.ThrowsAsync<AppException>(async () => await _service.Add(model));
    }

    [Fact]
    public async Task AddEquipmentApplication_SouldFailCancelTwice()
    {        

        _db.EquipmentApplications.Add(new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 1),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive = false
        });

        await _db.SaveChangesAsync();

        var model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 2),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive  = false
        };

        await  Assert.ThrowsAsync<AppException>(async () => await _service.Add(model));
    }

    [Fact]
    public async Task AddEquipmentApplication_ShouldPassAnotherDimentions()
    {        

        _db.EquipmentApplications.Add(new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 1),
            WorkTypeId = 1,
            EquipmentTypeId = 1,
            IsActive = true
        });

        await _db.SaveChangesAsync();

        var model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 2),
            WorkTypeId = 2,
            EquipmentTypeId = 2,
            IsActive = true
        };

        await _service.Add(model);

        Assert.Equal(2, _db.EquipmentApplications.Count());
    }

    [Fact]
    public async Task AddEquipmentApplication_ShouldPassIfCorrect()
    {        

        _db.EquipmentApplications.AddRange(
            new EquipmentApplication {StartDate = new DateTime(2020, 1, 1),WorkTypeId = 1,EquipmentTypeId = 1,IsActive = true},
            new EquipmentApplication {StartDate = new DateTime(2020, 1, 2),WorkTypeId = 1,EquipmentTypeId = 1,IsActive = false}
        );

        await _db.SaveChangesAsync();

        var model = new EquipmentApplication
        {
            StartDate = new DateTime(2020, 1, 3),WorkTypeId = 1,EquipmentTypeId = 1,IsActive = true};

        await _service.Add(model);

        Assert.Equal(3, _db.EquipmentApplications.Count());
    }
}