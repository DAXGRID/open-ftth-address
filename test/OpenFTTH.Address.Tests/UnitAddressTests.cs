using OpenFTTH.Address.Business;
using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Address.Tests;

[Order(20)]
public class UnitAddressTests
{
    private readonly IEventStore _eventStore;

    public UnitAddressTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_is_success()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Active;
        string? floorName = null;
        string? suitName = null;
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;

        var unitAddressAR = new UnitAddressAR();

        var createUnitAddressResult = unitAddressAR.Create(
            id: id,
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            created: created,
            updated: updated);

        _eventStore.Aggregates.Store(unitAddressAR);

        createUnitAddressResult.IsSuccess.Should().BeTrue();
        unitAddressAR.Id.Should().Be(id);
        unitAddressAR.OfficialId.Should().Be(officialId);
        unitAddressAR.AccessAddressId.Should().Be(accessAddressId);
        unitAddressAR.Status.Should().Be(status);
        unitAddressAR.FloorName.Should().Be(floorName);
        unitAddressAR.SuitName.Should().Be(suitName);
        unitAddressAR.Created.Should().Be(created);
        unitAddressAR.Updated.Should().Be(updated);
    }

    [Fact, Order(1)]
    public void Create_default_id_is_invalid()
    {
        var id = Guid.Empty;
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Active;
        string? floorName = null;
        string? suitName = null;
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;

        var unitAddressAR = new UnitAddressAR();

        var createUnitAddressResult = unitAddressAR.Create(
            id: id,
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            created: created,
            updated: updated);

        _eventStore.Aggregates.Store(unitAddressAR);

        createUnitAddressResult.IsSuccess.Should().BeFalse();
        createUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)createUnitAddressResult.Errors.First())
            .Code
            .Should()
            .Be(UnitAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);

    }

    [Fact, Order(1)]
    public void Create_access_address_default_id_is_invalid()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Empty;
        var status = Status.Active;
        string? floorName = null;
        string? suitName = null;
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;

        var unitAddressAR = new UnitAddressAR();

        var createUnitAddressResult = unitAddressAR.Create(
            id: id,
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            created: created,
            updated: updated);

        createUnitAddressResult.IsSuccess.Should().BeFalse();
        createUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)createUnitAddressResult.Errors.First())
            .Code
            .Should()
            .Be(UnitAddressErrorCodes.ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(1)]
    public void Create_default_created_date_is_invalid()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Active;
        string? floorName = null;
        string? suitName = null;
        var created = new DateTime();
        var updated = DateTime.UtcNow;

        var unitAddressAR = new UnitAddressAR();

        var createUnitAddressResult = unitAddressAR.Create(
            id: id,
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            created: created,
            updated: updated);

        createUnitAddressResult.IsSuccess.Should().BeFalse();
        createUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)createUnitAddressResult.Errors.First())
            .Code
            .Should()
            .Be(UnitAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_default_updated_date_is_invalid()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Active;
        string? floorName = null;
        string? suitName = null;
        var created = DateTime.UtcNow;
        var updated = new DateTime();

        var unitAddressAR = new UnitAddressAR();

        var createUnitAddressResult = unitAddressAR.Create(
            id: id,
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            created: created,
            updated: updated);

        createUnitAddressResult.IsSuccess.Should().BeFalse();
        createUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)createUnitAddressResult.Errors.First())
            .Code
            .Should()
            .Be(UnitAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Discontinued;
        string? floorName = null;
        string? suitName = null;
        var updated = DateTime.UtcNow;

        var unitAddressAR = _eventStore.Aggregates.Load<UnitAddressAR>(id);

        var updateUnitAddressResult = unitAddressAR.Update(
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            updated: updated);

        _eventStore.Aggregates.Store(unitAddressAR);

        updateUnitAddressResult.IsSuccess.Should().BeTrue();
        unitAddressAR.Id.Should().Be(id);
        unitAddressAR.OfficialId.Should().Be(officialId);
        unitAddressAR.AccessAddressId.Should().Be(accessAddressId);
        unitAddressAR.Status.Should().Be(status);
        unitAddressAR.FloorName.Should().Be(floorName);
        unitAddressAR.SuitName.Should().Be(suitName);
        unitAddressAR.Updated.Should().Be(updated);
    }

    [Fact, Order(2)]
    public void Update_before_created_is_invalid()
    {
        var officialId = Guid.Parse("89852ac6-254f-4938-aec8-4fac7cb72901");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Pending;
        string? floorName = null;
        string? suitName = null;
        var updated = DateTime.UtcNow;

        var unitAddressAR = new UnitAddressAR();

        var updateUnitAddressResult = unitAddressAR.Update(
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            updated: updated);

        updateUnitAddressResult.IsSuccess.Should().BeFalse();
        updateUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)updateUnitAddressResult.Errors.First())
            .Code.Should().Be(UnitAddressErrorCodes.ID_NOT_SET);
    }

    [Fact, Order(2)]
    public void Update_access_address_id_being_default_guid_is_invalid()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("89852ac6-254f-4938-aec8-4fac7cb72901");
        var accessAddressId = Guid.Empty;
        var status = Status.Pending;
        string? floorName = null;
        string? suitName = null;
        var updated = DateTime.UtcNow;

        var unitAddressAR = _eventStore.Aggregates.Load<UnitAddressAR>(id);

        var updateUnitAddressResult = unitAddressAR.Update(
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            updated: updated);

        updateUnitAddressResult.IsSuccess.Should().BeFalse();
        updateUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)updateUnitAddressResult.Errors.First())
            .Code.Should().Be(UnitAddressErrorCodes.ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(2)]
    public void Update_updated_being_default_is_invalid()
    {
        var id = Guid.Parse("d4de2559-066d-4492-8f84-712f4995b7a3");
        var officialId = Guid.Parse("89852ac6-254f-4938-aec8-4fac7cb72901");
        var accessAddressId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var status = Status.Pending;
        string? floorName = null;
        string? suitName = null;
        var updated = new DateTime();

        var unitAddressAR = _eventStore.Aggregates.Load<UnitAddressAR>(id);

        var updateUnitAddressResult = unitAddressAR.Update(
            officialId: officialId,
            accessAddressId: accessAddressId,
            status: status,
            floorName: floorName,
            suitName: suitName,
            updated: updated);

        updateUnitAddressResult.IsSuccess.Should().BeFalse();
        updateUnitAddressResult.Errors.Should().HaveCount(1);
        ((UnitAddressError)updateUnitAddressResult.Errors.First())
            .Code.Should().Be(UnitAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }
}
