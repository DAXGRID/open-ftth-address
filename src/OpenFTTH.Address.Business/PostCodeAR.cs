using FluentResults;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class PostCodeAR : AggregateBase
{
    public string? Number { get; private set; }
    public string? Name { get; private set; }

    public PostCodeAR()
    {
        Register<PostCodeCreated>(Apply);
        Register<PostCodeUpdated>(Apply);
    }

    public Result Create(Guid id, string number, string name)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(id)} cannot be empty guid."));
        }

        if (String.IsNullOrWhiteSpace(number))
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(number)} cannot be null empty or whitespace."));
        }

        if (String.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(name)} cannot be null empty or whitespace."));
        }

        Id = id;

        RaiseEvent(new PostCodeCreated(
                       id: id,
                       number: number,
                       name: name));

        return Result.Ok();
    }

    public Result Update(string name)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(name)} cannot be null empty or whitespace."));
        }

        RaiseEvent(new PostCodeUpdated(id: this.Id, name: name));

        return Result.Ok();
    }

    private void Apply(PostCodeCreated postCodeCreated)
    {
        Id = postCodeCreated.Id;
        Number = postCodeCreated.Number;
        Name = postCodeCreated.Name;
    }

    private void Apply(PostCodeUpdated postCodeUpdated)
    {
        Name = postCodeUpdated.Name;
    }
}
