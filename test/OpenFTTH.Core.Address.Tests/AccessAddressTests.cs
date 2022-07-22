using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

[Order(10)]
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
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

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
        accessAddressAR.PostCodeId.Should().Be(postCodeId);
        accessAddressAR.EastCoordinate.Should().Be(eastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(northCoordinate);
        accessAddressAR.LocationUpdated.Should().Be(locationUpdated);
        accessAddressAR.SupplementaryTownName.Should().Be(supplementaryTownName);
        accessAddressAR.PlotId.Should().Be(plotId);
        accessAddressAR.RoadId.Should().Be(roadId);
    }

    [Fact, Order(1)]
    public void Create_id_empty_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Empty;
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
           .Code.Should().Be(AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(1)]
    public void Create_created_date_is_default_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = new DateTime();
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_updated_date_is_default()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = new DateTime();
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_road_does_not_exist_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("e138802a-1717-49d6-9281-9a13dff2fdb9");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.ROAD_DOES_NOT_EXIST);
    }

    [Fact, Order(1)]
    public void Create_post_code_does_not_exist_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("082cb73e-caa8-4fff-9374-4f186567f719");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

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
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.POST_CODE_DOES_NOT_EXIST);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        _eventStore.Aggregates.Store(accessAddressAR);

        updateAccessAddressResult.IsSuccess.Should().BeTrue();
        accessAddressAR.Id.Should().Be(id);
        accessAddressAR.OfficialId.Should().Be(officialId);
        accessAddressAR.Updated.Should().Be(updated);
        accessAddressAR.MunicipalCode.Should().Be(municipalCode);
        accessAddressAR.Status.Should().Be(status);
        accessAddressAR.RoadCode.Should().Be(roadCode);
        accessAddressAR.HouseNumber.Should().Be(houseNumber);
        accessAddressAR.PostCodeId.Should().Be(postCodeId);
        accessAddressAR.EastCoordinate.Should().Be(eastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(northCoordinate);
        accessAddressAR.LocationUpdated.Should().Be(locationUpdated);
        accessAddressAR.SupplementaryTownName.Should().Be(supplementaryTownName);
        accessAddressAR.PlotId.Should().Be(plotId);
        accessAddressAR.RoadId.Should().Be(roadId);
    }

    [Fact, Order(2)]
    public void Update_id_not_set_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.AccessAddressIds;
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

        var accessAddressAR = new AccessAddressAR();

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(2)]
    public void Update_updated_being_default_date_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = new DateTime();
        var municipalCode = "F1234";
        var status = AddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(2)]
    public void Update_road_id_does_not_exist_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("4d137186-56b7-4753-80b8-b9785104868a");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.ROAD_DOES_NOT_EXIST);
    }

    [Fact, Order(2)]
    public void Update_road_does_not_exist_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var officialId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("082cb73e-caa8-4fff-9374-4f186567f719");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var locationUpdated = DateTime.UtcNow;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.RoadIdToOfficalId.Keys.ToHashSet();
        var existingPostCodeIds = addressProjection.PostCodeIdToNumber.Keys.ToHashSet();

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            officialId: officialId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            locationUpdated: locationUpdated,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.POST_CODE_DOES_NOT_EXIST);
    }
}
