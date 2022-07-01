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
        var id = Guid.NewGuid();
        var officialId = Guid.NewGuid();
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
        var roadId = Guid.NewGuid();

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

        // Assert
        createWorkProjectResult.IsSuccess.Should().BeTrue();
    }

    [Fact, Order(2)]
    public void Update_ShouldSucceed()
    {
        var id = Guid.NewGuid();
        var officialId = Guid.NewGuid();
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
        var roadId = Guid.NewGuid();

        var workProjectAR = new AccessAddressAR();

        var updateAccessAddressResult = workProjectAR.Update(
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

        _eventStore.Aggregates.Store(workProjectAR);

        updateAccessAddressResult.IsSuccess.Should().BeTrue();
    }
}
