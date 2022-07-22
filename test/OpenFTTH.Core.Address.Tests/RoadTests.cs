using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

[Order(0)]
public class RoadTests
{
    private readonly IEventStore _eventStore;

    public RoadTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_is_success()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var officialId = "F12345";
        var name = "Pilevej";
        var status = RoadStatus.Effective;

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(
            id: id,
            officialId: officialId,
            name: name,
            status: status);

        _eventStore.Aggregates.Store(roadAR);

        createRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(id);
        roadAR.OfficialId.Should().Be(officialId);
        roadAR.Name.Should().Be(name);
        roadAR.Status.Should().Be(status);
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
        var officialId = "F12345";
        var name = "Kolding";
        var status = RoadStatus.Effective;

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Update(name: name, status: status);

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
        var id = Guid.NewGuid(); // Random guid that is not created yet.
        var name = "Kolding";
        var status = RoadStatus.Effective;

        var roadAR = new RoadAR();

        var updateRoadResult = roadAR.Update(name: name, status: status);

        updateRoadResult.IsFailed.Should().BeTrue();
        updateRoadResult.Errors.Should().HaveCount(1);
        ((RoadError)updateRoadResult.Errors.First())
            .Code
            .Should()
            .Be(RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID);
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
    }

    [Fact, Order(3)]
    public void Delete_is_success()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");

        var roadAR = _eventStore.Aggregates.Load<RoadAR>(id);

        var updateRoadResult = roadAR.Delete();

        updateRoadResult.IsSuccess.Should().BeTrue();
    }
}
