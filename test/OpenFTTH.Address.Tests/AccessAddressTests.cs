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
    public void Create_is_success()
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

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
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

        _eventStore.Aggregates.Store(accessAddressAR);

        createAccessAddressResult.IsSuccess.Should().BeTrue();
        accessAddressAR.Id.Should().Be(id);
        accessAddressAR.OfficialId.Should().Be(officialId);
        accessAddressAR.Created.Should().Be(created);
        accessAddressAR.Updated.Should().Be(updated);
        accessAddressAR.MunicipalCode.Should().Be(municipalCode);
        accessAddressAR.Status.Should().Be(status);
        accessAddressAR.RoadCode.Should().Be(roadCode);
        accessAddressAR.HouseNumber.Should().Be(houseNumber);
        accessAddressAR.PostDistrictCode.Should().Be(postDistrictCode);
        accessAddressAR.EastCoordinate.Should().Be(eastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(northCoordinate);
        accessAddressAR.LocationUpdated.Should().Be(locationUpdated);
        accessAddressAR.TownName.Should().Be(townName);
        accessAddressAR.PlotId.Should().Be(plotId);
        accessAddressAR.RoadId.Should().Be(roadId);
    }

    [Fact, Order(1)]
    public void Create_is_invalid_id_empty()
    {
        var id = Guid.Empty;
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

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
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

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
           .Code.Should().Be(AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(1)]
    public void Create_created_date_is_default()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var created = new DateTime();
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

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
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

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_updated_date_is_default()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var created = DateTime.UtcNow;
        var updated = new DateTime();
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

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
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

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = Status.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postDistrictCode = "6000";
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var townName = "Kolding";
        var plotId = "12445F";
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
        accessAddressAR.Id.Should().Be(id);
        accessAddressAR.OfficialId.Should().Be(officialId);
        accessAddressAR.Updated.Should().Be(updated);
        accessAddressAR.MunicipalCode.Should().Be(municipalCode);
        accessAddressAR.Status.Should().Be(status);
        accessAddressAR.RoadCode.Should().Be(roadCode);
        accessAddressAR.HouseNumber.Should().Be(houseNumber);
        accessAddressAR.PostDistrictCode.Should().Be(postDistrictCode);
        accessAddressAR.EastCoordinate.Should().Be(eastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(northCoordinate);
        accessAddressAR.LocationUpdated.Should().Be(locationUpdated);
        accessAddressAR.TownName.Should().Be(townName);
        accessAddressAR.PlotId.Should().Be(plotId);
        accessAddressAR.RoadId.Should().Be(roadId);
    }

    [Fact, Order(2)]
    public void Update_id_not_set_invalid()
    {
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = Status.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postDistrictCode = "6000";
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var townName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("5a4532f5-9355-49e3-9e1a-8cc62c843f9a");

        var accessAddressAR = new AccessAddressAR();

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

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(2)]
    public void Update_updated_being_default_date_is_invalid()
    {
        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var updated = new DateTime();
        var municipalCode = "F1234";
        var status = Status.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postDistrictCode = "6000";
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var townName = "Kolding";
        var plotId = "12445F";
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

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }
}
