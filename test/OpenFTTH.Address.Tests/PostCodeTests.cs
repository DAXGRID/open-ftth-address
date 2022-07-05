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
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var number = "7000";
        var name = "Fredericia";

        var workProjectAR = new PostCodeAR();

        var createWorkProjectResult = workProjectAR.Create(
            id: id,
            number: number,
            name: name);

        _eventStore.Aggregates.Store(workProjectAR);

        createWorkProjectResult.IsSuccess.Should().BeTrue();
    }

    [Fact, Order(2)]
    public void Update_ShouldSucceed()
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var name = "Fredericia";

        var postCodeAR = _eventStore.Aggregates.Load<PostCodeAR>(id);

        var updatePostCodeResult = postCodeAR.Update(name: name);

        _eventStore.Aggregates.Store(postCodeAR);

        updatePostCodeResult.IsSuccess.Should().BeTrue();
    }
}
