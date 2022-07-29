using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

public record CreateRoadAddressExampleData
{
    public Guid Id { get; init; }
    public string? OfficialId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }

    public CreateRoadAddressExampleData(
        Guid id,
        string? officialId,
        string name,
        RoadStatus status)
    {
        Id = id;
        OfficialId = officialId;
        Name = name;
        Status = status;
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
                officialId: "F12345",
                name: "Pilevej",
                status: RoadStatus.Effective)
        };

        yield return new object[]
        {
            new CreateRoadAddressExampleData(
                id: Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073"),
                officialId: "A12345",
                name: "Blomsterhaven",
                status: RoadStatus.Temporary)
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
            officialId: roadExampleData.OfficialId,
            name: roadExampleData.Name,
            status: roadExampleData.Status);

        _eventStore.Aggregates.Store(roadAR);

        createRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(roadExampleData.Id);
        roadAR.OfficialId.Should().Be(roadExampleData.OfficialId);
        roadAR.Name.Should().Be(roadExampleData.Name);
        roadAR.Status.Should().Be(roadExampleData.Status);
    }

    [Fact, Order(1)]
    public void Create_default_id_is_invalid()
    {
        var id = Guid.Empty;
        var officialId = "F12345";
        var name = "Pilevej";
        var status = RoadStatus.Effective;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            officialId: officialId,
            name: name,
            status: status);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Theory, Order(1)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Create_empty_or_whitespace_official_id_is_invalid(string officialId)
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var name = "Pilevej";
        var status = RoadStatus.Effective;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            officialId: officialId,
            name: name,
            status: status);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)createRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.OFFICIAL_ID_CANNOT_BE_WHITE_SPACE_OR_NULL);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var officialId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            officialId: officialId,
            name: name,
            status: status);

        _eventStore.Aggregates.Store(roadAR);

        updateRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(id);
        roadAR.OfficialId.Should().Be(officialId);
        roadAR.Name.Should().Be(name);
        roadAR.Status.Should().Be(status);
    }

    [Fact, Order(2)]
    public void Update_is_invalid_not_created_yet()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var officialId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;

        var roadAR = new RoadAR();

        var updateRoadResult = roadAR.Update(
            officialId: officialId,
            name: name,
            status: status);

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
        var officialId = "F123456";
        var name = "Kolding 2";
        var status = RoadStatus.Temporary;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            officialId: officialId,
            name: name,
            status: status);

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

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete();

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
        roadAR.Deleted.Should().BeFalse();
    }

    [Fact, Order(3)]
    public void Delete_is_success()
    {
        var id = Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073");

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete();

        _eventStore.Aggregates.Store(roadAR);

        updateRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Deleted.Should().BeTrue();
    }

    [Fact, Order(4)]
    public void Cannot_update_deleted()
    {
        var id = Guid.Parse("40fc2260-870c-413e-953e-5b17daa57073");
        var officialId = "F12345";
        var name = "Kolding";
        var status = RoadStatus.Effective;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(
            name: name,
            officialId: officialId,
            status: status);

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

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete();

        updateRoadResult.IsSuccess.Should().BeFalse();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.CANNOT_DELETE_ALREADY_DELETED);
        roadAR.Deleted.Should().BeTrue();
    }
}
