using OpenFTTH.Address.Business;
using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Address.Tests;

[Order(0)]
public class AcessAddressTests
{
    private readonly IEventStore _eventStore;

    public AcessAddressTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_ShouldSucceed()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = Status.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postDistrictCode = "7000";
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var townName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("5a4532f5-9355-49e3-9e1a-8cc62c843f9a");

        var workProjectAR = new AccessAddressAR();

        var createWorkProjectResult = workProjectAR.Create(
            id: id,
            officialId: officialId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postDistrictCode: postDistrictCode,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            townName: townName,
            plotId: plotId,
            roadId: roadId);

        _eventStore.Aggregates.Store(workProjectAR);

        createWorkProjectResult.IsSuccess.Should().BeTrue();
    }

    [Fact, Order(2)]
    public void Update_ShouldSucceed()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = Status.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postDistrictCode = "7000";
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var townName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("5a4532f5-9355-49e3-9e1a-8cc62c843f9a");

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postDistrictCode: postDistrictCode,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            townName: townName,
            plotId: plotId,
            roadId: roadId);

        _eventStore.Aggregates.Store(accessAddressAR);

        updateAccessAddressResult.IsSuccess.Should().BeTrue();
    }
}
