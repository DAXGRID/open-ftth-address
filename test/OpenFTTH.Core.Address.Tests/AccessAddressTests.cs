using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

public record CreateAccessAddressExampleData
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public string MunicipalCode { get; init; }
    public AccessAddressStatus Status { get; init; }
    public string RoadCode { get; init; }
    public string HouseNumber { get; init; }
    public Guid PostCodeId { get; init; }
    public double EastCoordinate { get; init; }
    public double NorthCoordinate { get; init; }
    public string? SupplementaryTownName { get; init; }
    public string? PlotId { get; init; }
    public Guid RoadId { get; init; }
    public bool PendingOfficial { get; init; }

    public CreateAccessAddressExampleData(
        Guid id,
        string? externalId,
        DateTime created,
        DateTime updated,
        string municipalCode,
        AccessAddressStatus status,
        string roadCode,
        string houseNumber,
        Guid postCodeId,
        double eastCoordinate,
        double northCoordinate,
        string? supplementaryTownName,
        string? plotId,
        Guid roadId,
        bool pendingOfficial)
    {
        Id = id;
        ExternalId = externalId;
        Created = created;
        Updated = updated;
        MunicipalCode = municipalCode;
        Status = status;
        RoadCode = roadCode;
        HouseNumber = houseNumber;
        PostCodeId = postCodeId;
        EastCoordinate = eastCoordinate;
        NorthCoordinate = northCoordinate;
        SupplementaryTownName = supplementaryTownName;
        PlotId = plotId;
        RoadId = roadId;
        PendingOfficial = pendingOfficial;
    }
}

[Order(10)]
public class AcessAddressTests
{
    private readonly IEventStore _eventStore;

    public AcessAddressTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    private static IEnumerable<object[]> ExampleCreateValues()
    {
        yield return new object[]
        {
            new CreateAccessAddressExampleData(
                id: Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc"),
                externalId: "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc",
                created: DateTime.UtcNow,
                updated: DateTime.UtcNow,
                municipalCode: "D1234",
                status: AccessAddressStatus.Active,
                roadCode: "D12",
                houseNumber: "12F",
                postCodeId: Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002"),
                eastCoordinate: 10.20,
                northCoordinate: 20.10,
                supplementaryTownName: "Fredericia",
                plotId: "12455F",
                roadId: Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5"),
                pendingOfficial: true)
        };

        yield return new object[]
        {
            new CreateAccessAddressExampleData(
                id: Guid.Parse("94b1f97d-42df-49b3-90c6-74266a16661d"),
                externalId: "476acb63-c29f-4d54-8f13-92a6b3022a0e",
                created: DateTime.UtcNow,
                updated: DateTime.UtcNow,
                municipalCode: "F1234",
                status: AccessAddressStatus.Active,
                roadCode: "A12",
                houseNumber: "20F",
                postCodeId: Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002"),
                eastCoordinate: 10.20,
                northCoordinate: 20.10,
                supplementaryTownName: "Fredericia",
                plotId: "455A",
                roadId: Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5"),
                pendingOfficial: false)
        };
    }

    [Theory, Order(1)]
    [MemberData(nameof(ExampleCreateValues))]
    public void Create_is_success_one(CreateAccessAddressExampleData createExampleData)
    {
        if (createExampleData is null)
        {
            throw new ArgumentNullException(nameof(createExampleData));
        }

        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: createExampleData.Id,
            externalId: createExampleData.ExternalId,
            created: createExampleData.Created,
            updated: createExampleData.Updated,
            municipalCode: createExampleData.MunicipalCode,
            status: createExampleData.Status,
            roadCode: createExampleData.RoadCode,
            houseNumber: createExampleData.HouseNumber,
            postCodeId: createExampleData.PostCodeId,
            eastCoordinate: createExampleData.EastCoordinate,
            northCoordinate: createExampleData.NorthCoordinate,
            supplementaryTownName: createExampleData.SupplementaryTownName,
            plotId: createExampleData.PlotId,
            roadId: createExampleData.RoadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: createExampleData.PendingOfficial);

        _eventStore.Aggregates.Store(accessAddressAR);

        createAccessAddressResult.IsSuccess.Should().BeTrue();
        accessAddressAR.Id.Should().Be(createExampleData.Id);
        accessAddressAR.ExternalId.Should().Be(createExampleData.ExternalId);
        accessAddressAR.Created.Should().Be(createExampleData.Created);
        accessAddressAR.Updated.Should().Be(createExampleData.Updated);
        accessAddressAR.MunicipalCode.Should().Be(createExampleData.MunicipalCode);
        accessAddressAR.Status.Should().Be(createExampleData.Status);
        accessAddressAR.RoadCode.Should().Be(createExampleData.RoadCode);
        accessAddressAR.HouseNumber.Should().Be(createExampleData.HouseNumber);
        accessAddressAR.PostCodeId.Should().Be(createExampleData.PostCodeId);
        accessAddressAR.EastCoordinate.Should().Be(createExampleData.EastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(createExampleData.NorthCoordinate);
        accessAddressAR.SupplementaryTownName
            .Should()
            .Be(createExampleData.SupplementaryTownName);
        accessAddressAR.PlotId.Should().Be(createExampleData.PlotId);
        accessAddressAR.RoadId.Should().Be(createExampleData.RoadId);
        accessAddressAR.PendingOfficial.Should().Be(createExampleData.PendingOfficial);
    }

    [Fact, Order(1)]
    public void Create_id_empty_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Empty;
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AccessAddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: id,
            externalId: externalId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = new DateTime();
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AccessAddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: id,
            externalId: externalId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

        createAccessAddressResult.IsSuccess.Should().BeFalse();
        createAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)createAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_updated_date_is_default_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = new DateTime();
        var municipalCode = "D1234";
        var status = AccessAddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: id,
            externalId: externalId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AccessAddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("e138802a-1717-49d6-9281-9a13dff2fdb9");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: id,
            externalId: externalId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;
        var municipalCode = "D1234";
        var status = AccessAddressStatus.Active;
        var roadCode = "D12";
        var houseNumber = "12F";
        var postCodeId = Guid.Parse("082cb73e-caa8-4fff-9374-4f186567f719");
        var eastCoordinate = 10.20;
        var northCoordinate = 20.10;
        var supplementaryTownName = "Fredericia";
        var plotId = "12455F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var createAccessAddressResult = accessAddressAR.Create(
            id: id,
            externalId: externalId,
            created: created,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.Today;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

        _eventStore.Aggregates.Store(accessAddressAR);

        updateAccessAddressResult.IsSuccess.Should().BeTrue();
        accessAddressAR.Id.Should().Be(id);
        accessAddressAR.ExternalId.Should().Be(externalId);
        accessAddressAR.Updated.Should().Be(updated);
        accessAddressAR.MunicipalCode.Should().Be(municipalCode);
        accessAddressAR.Status.Should().Be(status);
        accessAddressAR.RoadCode.Should().Be(roadCode);
        accessAddressAR.HouseNumber.Should().Be(houseNumber);
        accessAddressAR.PostCodeId.Should().Be(postCodeId);
        accessAddressAR.EastCoordinate.Should().Be(eastCoordinate);
        accessAddressAR.NorthCoordinate.Should().Be(northCoordinate);
        accessAddressAR.SupplementaryTownName.Should().Be(supplementaryTownName);
        accessAddressAR.PlotId.Should().Be(plotId);
        accessAddressAR.RoadId.Should().Be(roadId);
        accessAddressAR.PendingOfficial.Should().Be(pendingOfficial);
    }

    [Fact, Order(2)]
    public void Update_id_not_set_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = new AccessAddressAR();

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = new DateTime();
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("4d137186-56b7-4753-80b8-b9785104868a");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

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
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("082cb73e-caa8-4fff-9374-4f186567f719");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.POST_CODE_DOES_NOT_EXIST);
    }

    [Fact, Order(3)]
    public void Update_with_no_changes_is_invalid()
    {
        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var id = Guid.Parse("5bc2ad5b-8634-4b05-86b2-ea6eb10596dc");
        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.Today;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.NO_CHANGES);
    }

    [Fact, Order(3)]
    public void Cannot_delete_accesss_address_that_has_not_been_created()
    {
        var id = Guid.NewGuid();
        var updated = DateTime.UtcNow;

        var accessAddressAR = new AccessAddressAR();

        var deleteResult = accessAddressAR.Delete(updated);

        deleteResult.IsSuccess.Should().BeFalse();
        deleteResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)deleteResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
        accessAddressAR.Deleted.Should().BeFalse();
    }

    [Fact, Order(3)]
    public void Delete_with_default_updated_date_is_invalid()
    {
        var id = Guid.Parse("94b1f97d-42df-49b3-90c6-74266a16661d");
        var updated = new DateTime();

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var deleteResult = accessAddressAR.Delete(updated);

        _eventStore.Aggregates.Store(accessAddressAR);

        deleteResult.IsSuccess.Should().BeFalse();
        deleteResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)deleteResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE);
        accessAddressAR.Deleted.Should().BeFalse();
    }

    [Fact, Order(4)]
    public void Delete_is_success()
    {
        var id = Guid.Parse("94b1f97d-42df-49b3-90c6-74266a16661d");
        var updated = DateTime.UtcNow;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var deleteResult = accessAddressAR.Delete(updated);

        _eventStore.Aggregates.Store(accessAddressAR);

        deleteResult.IsSuccess.Should().BeTrue();
        accessAddressAR.Deleted.Should().BeTrue();
        accessAddressAR.Updated.Should().Be(updated);
    }

    [Fact, Order(5)]
    public void Cannot_delete_already_deleted()
    {
        var id = Guid.Parse("94b1f97d-42df-49b3-90c6-74266a16661d");
        var updated = DateTime.UtcNow;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var deleteResult = accessAddressAR.Delete(updated);

        deleteResult.IsSuccess.Should().BeFalse();
        deleteResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)deleteResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.CANNOT_DELETE_ALREADY_DELETED);
        accessAddressAR.Deleted.Should().BeTrue();
    }

    [Fact, Order(5)]
    public void Cannot_update_when_deleted()
    {
        var id = Guid.Parse("94b1f97d-42df-49b3-90c6-74266a16661d");

        var addressProjection = _eventStore.Projections.Get<AddressProjection>();

        var externalId = "5bc2ad5b-8634-4b05-86b2-ea6eb10596dc";
        var updated = DateTime.UtcNow;
        var municipalCode = "F1234";
        var status = AccessAddressStatus.Discontinued;
        var roadCode = "F12";
        var houseNumber = "10F";
        var postCodeId = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var eastCoordinate = 50.20;
        var northCoordinate = 50.10;
        var supplementaryTownName = "Kolding";
        var plotId = "12445F";
        var roadId = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var existingRoadIds = addressProjection.GetRoadIds();
        var existingPostCodeIds = addressProjection.GetPostCodeIds();
        var pendingOfficial = false;

        var accessAddressAR = _eventStore.Aggregates.Load<AccessAddressAR>(id);

        var updateAccessAddressResult = accessAddressAR.Update(
            externalId: externalId,
            updated: updated,
            municipalCode: municipalCode,
            status: status,
            roadCode: roadCode,
            houseNumber: houseNumber,
            postCodeId: postCodeId,
            eastCoordinate: eastCoordinate,
            northCoordinate: northCoordinate,
            supplementaryTownName: supplementaryTownName,
            plotId: plotId,
            roadId: roadId,
            existingRoadIds: existingRoadIds,
            existingPostCodeIds: existingPostCodeIds,
            pendingOfficial: pendingOfficial);

        updateAccessAddressResult.IsSuccess.Should().BeFalse();
        updateAccessAddressResult.Errors.Count.Should().Be(1);
        ((AccessAddressError)updateAccessAddressResult.Errors.First())
            .Code.Should().Be(AccessAddressErrorCodes.CANNOT_UPDATE_DELETED);
    }
}
