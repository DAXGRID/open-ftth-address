using OpenFTTH.EventSourcing;
using Xunit.Extensions.Ordering;

namespace OpenFTTH.Core.Address.Tests;

[Order(0)]
public class PostCodeTests
{
    private readonly IEventStore _eventStore;

    public PostCodeTests(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [Fact, Order(1)]
    public void Create_is_success()
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var number = "7000";
        var name = "Fredericia";

        var postCodeAR = new PostCodeAR();

        var createPostCodeResult = postCodeAR.Create(
            id: id,
            number: number,
            name: name);

        _eventStore.Aggregates.Store(postCodeAR);

        createPostCodeResult.IsSuccess.Should().BeTrue();
        postCodeAR.Id.Should().Be(id);
        postCodeAR.Number.Should().Be(number);
        postCodeAR.Name.Should().Be(name);
    }

    [Fact, Order(1)]
    public void Create_default_id_is_invalid()
    {
        var id = Guid.Empty;
        var number = "7000";
        var name = "Fredericia";

        var postCodeAR = new PostCodeAR();

        var createPostCodeResult = postCodeAR.Create(
            id: id,
            number: number,
            name: name);

        createPostCodeResult.IsSuccess.Should().BeFalse();
        createPostCodeResult.Errors.Should().HaveCount(1);
        ((PostCodeError)createPostCodeResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Theory, Order(1)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Create_number_empty_or_whitespace_is_invalid(string number)
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var name = "Fredericia";

        var postCodeAR = new PostCodeAR();

        var createPostCodeResult = postCodeAR.Create(
            id: id,
            number: number,
            name: name);

        createPostCodeResult.IsSuccess.Should().BeFalse();
        createPostCodeResult.Errors.Should().HaveCount(1);
        ((PostCodeError)createPostCodeResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.NUMBER_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE);
    }

    [Theory, Order(1)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Create_name_empty_or_whitespace_invalid(string name)
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var number = "7000";

        var postCodeAR = new PostCodeAR();

        var createPostCodeResult = postCodeAR.Create(
            id: id,
            number: number,
            name: name);

        createPostCodeResult.IsSuccess.Should().BeFalse();
        createPostCodeResult.Errors.Should().HaveCount(1);
        ((PostCodeError)createPostCodeResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.NAME_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE);
    }

    [Fact, Order(2)]
    public void Update_is_success()
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var number = "7000";
        var name = "New Fredericia";

        var postCodeAR = _eventStore.Aggregates.Load<PostCodeAR>(id);

        var updatePostCodeResult = postCodeAR.Update(name: name);

        _eventStore.Aggregates.Store(postCodeAR);

        updatePostCodeResult.IsSuccess.Should().BeTrue();
        postCodeAR.Id.Should().Be(id);
        postCodeAR.Name.Should().Be(name);
        postCodeAR.Number.Should().Be(number);
    }

    [Fact, Order(2)]
    public void Update_guid_is_empty_is_invalid()
    {
        var name = "New Fredericia";
        var postCodeAR = new PostCodeAR();

        var updatePostCodeResult = postCodeAR.Update(name: name);

        updatePostCodeResult.IsSuccess.Should().BeFalse();
        updatePostCodeResult.Errors.Should().HaveCount(1);
        ((PostCodeError)updatePostCodeResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(2)]
    public void Update_name_is_empty_null_or_whitespace_is_invalid()
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");
        var name = String.Empty;

        var postCodeAR = _eventStore.Aggregates.Load<PostCodeAR>(id);

        var updatePostCodeResult = postCodeAR.Update(name: name);

        updatePostCodeResult.IsSuccess.Should().BeFalse();
        updatePostCodeResult.Errors.Should().HaveCount(1);
        ((PostCodeError)updatePostCodeResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.NAME_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE);
    }

    [Fact, Order(3)]
    public void Cannot_delete_post_code_that_has_not_yet_been_created()
    {
        var id = Guid.Parse("53d46647-edb3-428e-8063-b25e1009029e");

        var postCodeAR = _eventStore.Aggregates.Load<PostCodeAR>(id);
        var deleteResult = postCodeAR.Delete();

        deleteResult.IsSuccess.Should().BeFalse();
        deleteResult.Errors.Should().HaveCount(1);
        ((PostCodeError)deleteResult.Errors.First())
            .Code
            .Should()
            .Be(PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID);
    }

    [Fact, Order(3)]
    public void Delete_is_successful()
    {
        var id = Guid.Parse("1acef11e-fc4e-11ec-b939-0242ac120002");

        var postCodeAR = _eventStore.Aggregates.Load<PostCodeAR>(id);

        var deleteResult = postCodeAR.Delete();

        deleteResult.IsSuccess.Should().BeTrue();
    }
}
