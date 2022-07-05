using OpenFTTH.Address.Business;
using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Address.Tests;

[Order(10)]
public class RoadTests
{
    private readonly IEventStore _eventStore;

    public RoadTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_is_valid()
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F12345";
        var name = "Pilevej";

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(id: id, externalId: externalId, name: name);

        _eventStore.Aggregates.Store(roadAR);

        createRoadResult.IsSuccess.Should().BeTrue();
        roadAR.Id.Should().Be(id);
        roadAR.ExternalId.Should().Be(externalId);
        roadAR.Name.Should().Be(name);
    }

    [Fact, Order(1)]
    public void Create_default_id_is_invalid()
    {
        var id = Guid.Empty;
        var externalId = "F12345";
        var name = "Pilevej";

        var expected = new List<FluentResults.IError>
        {
            new RoadError(
                RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                $"{nameof(id)} cannot be empty guid.")
        };

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(id: id, externalId: externalId, name: name);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().BeEquivalentTo(expected);
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

        var expected = new List<FluentResults.IError>
        {
            new RoadError(
                RoadErrorCode.EXTERNAL_ID_CANNOT_BE_WHITE_SPACE_OR_NULL,
                $"{nameof(externalId)} is not allowed to be whitespace or null.")
        };

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(id: id, externalId: externalId, name: name);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().BeEquivalentTo(expected);
    }

    [Theory, Order(1)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Create_name_whitespace_or_null_is_invalid(string name)
    {
        var id = Guid.Parse("d309aa7b-81a3-4708-b1f5-e8155c29e5b5");
        var externalId = "F12345";

        var expected = new List<FluentResults.IError>
        {
            new RoadError(
                RoadErrorCode.NAME_CANNOT_BE_WHITE_SPACE_OR_NULL,
                $"{nameof(name)} is not allowed to be whitespace or null.")
        };

        var roadAR = new RoadAR();
        var createRoadResult = roadAR.Create(id: id, externalId: externalId, name: name);

        createRoadResult.IsFailed.Should().BeTrue();
        createRoadResult.Errors.Should().BeEquivalentTo(expected);
    }
}
