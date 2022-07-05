using OpenFTTH.Address.Business;
using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Address.Tests;

[Order(10)]
public class PostCodeTests
{
    private readonly IEventStore _eventStore;

    public PostCodeTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_ShouldSucceed()
    {
        var id = Guid.NewGuid();
        var number = "7000";
        var name = "Fredericia";

        var workProjectAR = new PostCodeAR();

        var createWorkProjectResult = workProjectAR.Create(
            id: id,
            number: number,
            name: name);

        _eventStore.Aggregates.Store(workProjectAR);

        // Assert
        createWorkProjectResult.IsSuccess.Should().BeTrue();
    }

    [Fact, Order(2)]
    public void Update_ShouldSucceed()
    {
        var id = Guid.NewGuid();
        var number = "7000";
        var name = "Fredericia";

        var workProjectAR = new PostCodeAR();

        var createWorkProjectResult = workProjectAR.Create(
            id: id,
            number: number,
            name: name);

        _eventStore.Aggregates.Store(workProjectAR);

        // Assert
        createWorkProjectResult.IsSuccess.Should().BeTrue();
    }
}
