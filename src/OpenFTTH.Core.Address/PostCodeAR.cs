using FluentResults;
using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class PostCodeAR : AggregateBase
{
    public string Number { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public bool Deleted { get; private set; }
    public DateTime? ExternalCreatedDate { get; private set; }
    public DateTime? ExternalUpdatedDate { get; private set; }

    public PostCodeAR()
    {
        Register<PostCodeCreated>(Apply);
        Register<PostCodeUpdated>(Apply);
        Register<PostCodeDeleted>(Apply);
    }

    public Result Create(
        Guid id,
        string number,
        string name,
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate)
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
                    PostCodeErrorCodes.NUMBER_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(number)} cannot be null empty or whitespace."));
        }

        if (String.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.NAME_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(name)} cannot be null empty or whitespace."));
        }

        RaiseEvent(new PostCodeCreated(
                       id: id,
                       number: number,
                       name: name,
                       externalCreatedDate: externalCreatedDate,
                       externalUpdatedDate: externalUpdatedDate));

        return Result.Ok();
    }

    public Result Update(string name, DateTime? externalUpdatedDate)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.CANNOT_UPDATE_DELETED,
                    $"Id '{Id}': cannot update deleted."));
        }

        if (String.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.NAME_CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
                    $"{nameof(name)} cannot be null empty or whitespace."));
        }

        var hasChanges = () =>
        {
            if (Name != name)
            {
                return true;
            }

            return false;
        };

        if (!hasChanges())
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.NO_CHANGES,
                    "No changes to the AR doing update."));
        }

        RaiseEvent(new PostCodeUpdated(
                       id: Id,
                       name: name,
                       externalUpdatedDate: externalUpdatedDate));

        return Result.Ok();
    }

    public Result Delete(DateTime? externalUpdatedDate)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new PostCodeError(
                    PostCodeErrorCodes.CANNOT_DELETE_ALREADY_DELETED,
                    @$"Id: '{Id}' is already deleted."));
        }

        RaiseEvent(new PostCodeDeleted(Id, externalUpdatedDate));

        return Result.Ok();
    }

    private void Apply(PostCodeCreated postCodeCreated)
    {
        Id = postCodeCreated.Id;
        Number = postCodeCreated.Number;
        Name = postCodeCreated.Name;
        ExternalCreatedDate = postCodeCreated.ExternalCreatedDate;
        ExternalUpdatedDate = postCodeCreated.ExternalUpdatedDate;
    }

    private void Apply(PostCodeUpdated postCodeUpdated)
    {
        Name = postCodeUpdated.Name;
        ExternalUpdatedDate = postCodeUpdated.ExternalUpdatedDate;
    }

    private void Apply(PostCodeDeleted postCodeDeleted)
    {
        Deleted = true;
        ExternalUpdatedDate = postCodeDeleted.ExternalUpdatedDate;
    }
}
