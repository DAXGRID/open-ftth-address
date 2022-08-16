using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

public record CreateRoadAddressExampleData
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public CreateRoadAddressExampleData(
        Guid id,
        string? externalId,
        string name,
        RoadStatus status,
        DateTime created,
        DateTime updated)
    {
        Id = id;
        ExternalId = externalId;
        Name = name;
        Status = status;
        Created = created;
        Updated = updated;
    }
}

[Order(0)]
public class RoadTests
{
    private readonly IEventStore _eventStore;

    public RoadTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public static IEnumerable<object[]> ExampleCreateValues()
    {
        yield return new object[]
        {
            new CreateRoadAddressExampleData(
                id: Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5"),
                externalId: "F12345",
                name: "Pilevej",
                status: RoadStatus.Effective,
                created: DateTime.UtcNow,
                updated: DateTime.UtcNow)
        };

        yield return new object[]
        {
            new CreateRoadAddressExampleData(
                id: Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073"),
                externalId: "A12345",
                name: "Blomsterhaven",
                status: RoadStatus.Temporary,
                created: DateTime.UtcNow,
                updated: DateTime.UtcNow)
        };
    }

    [Theory, Order(1)]
    [MemberData(nameof(ExampleCreateValues))]
    public void Create_is_success(CreateRoadAddressExampleData roadExampleData)
    {
        if (roadExampleData is null)
        {
            throw new ArgumentNullException(nameof(roadExampleData));
        }

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: roadExampleData.Id,
            externalId: roadExampleData.ExternalId,
            name: roadExampleData.Name,
            status: roadExampleData.Status,
            created: roadExampleData.Created,
            updated: roadExampleData.Updated);

        _eventStore.Aggregates.Store(roadAR);

        createRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(roadExampleData.Id);
        roadAR.ExternalId.Should().Be(roadExampleData.ExternalId);
        roadAR.Name.Should().Be(roadExampleData.Name);
        roadAR.Status.Should().Be(roadExampleData.Status);
        roadAR.Created.Should().Be(roadExampleData.Created);
        roadAR.Updated.Should().Be(roadExampleData.Updated);
    }

    [Fact, Order(1)]
    public void Create_default_id_is_invalid()
    {
        var id = Guid.Empty;
        var externalId = "F12345";
        var name = "Pilevej";
        var status = RoadStatus.Effective;
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            created: created,
            updated: updated);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(1)]
    public void Create_created_being_default_is_invalid()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F12345";
        var name = "Pilevej";
        var status = RoadStatus.Effective;
        var created = new DateTime();
        var updated = DateTime.UtcNow;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            created: created,
            updated: updated);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.CREATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(1)]
    public void Create_updated_being_default_is_invalid()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F12345";
        var name = "Pilevej";
        var status = RoadStatus.Effective;
        var created = DateTime.UtcNow;
        var updated = new DateTime();

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            created: created,
            updated: updated);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Theory, Order(1)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Create_empty_or_whitespace_external_id_is_invalid(string externalId)
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var name = "Pilevej";
        var status = RoadStatus.Effective;
        var created = DateTime.UtcNow;
        var updated = DateTime.UtcNow;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            created: created,
            updated: updated);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.EXTERNAL_ID_CANNOT_BE_WHITE_SPACE_OR_NULL);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            externalId: externalId,
            name: name,
            status: status,
            updated: updated);

        _eventStore.Aggregates.Store(roadAR);

        updateRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(id);
        roadAR.ExternalId.Should().Be(externalId);
        roadAR.Name.Should().Be(name);
        roadAR.Status.Should().Be(status);
        roadAR.Updated.Should().Be(updated);
    }

    [Fact, Order(2)]
    public void Update_updated_date_being_default_is_invalid()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;
        var updated = new DateTime();

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            externalId: externalId,
            name: name,
            status: status,
            updated: updated);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(2)]
    public void Update_is_invalid_not_created_yet()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;
        var updated = DateTime.UtcNow;

        var roadAR = new RoadAR();

        var updateRoadResult = roadAR.Update(
            externalId: externalId,
            name: name,
            status: status,
            updated: updated);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(3)]
    public void Update_is_invalid_when_no_changes()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            externalId: externalId,
            name: name,
            status: status,
            updated: updated);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.NO_CHANGES);
    }

    [Fact, Order(3)]
    public void Delete_is_invalid_not_created()
    {
        var id = Guid.NewGuid();
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete(updated: updated);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
        roadAR.Deleted.Should().BeFalse();
    }

    [Fact, Order(2)]
    public void Delete_updated_date_being_default_is_invalid()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var updated = new DateTime();

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete(
            updated: updated);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE);
    }

    [Fact, Order(3)]
    public void Delete_is_success()
    {
        var id = Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073");
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete(updated: updated);

        _eventStore.Aggregates.Store(roadAR);

        updateRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Deleted.Should().BeTrue();
        roadAR.Updated.Should().Be(updated);
    }

    [Fact, Order(4)]
    public void Cannot_update_deleted()
    {
        var id = Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073");
        var externalId = "F12345";
        var name = "Kolding";
        var status = RoadStatus.Effective;
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            name: name,
            externalId: externalId,
            status: status,
            updated: updated);

        updateRoadResult.IsSuccess.Should().BeFalse();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.CANNOT_UPDATE_DELETED);
        roadAR.Deleted.Should().BeTrue();
    }

    [Fact, Order(4)]
    public void Cannot_delete_already_deleted()
    {
        var id = Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073");
        var updated = DateTime.UtcNow;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete(updated);

        updateRoadResult.IsSuccess.Should().BeFalse();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.CANNOT_DELETE_ALREADY_DELETED);
        roadAR.Deleted.Should().BeTrue();
    }
}
