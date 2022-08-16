using FluentResults;
using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public enum RoadStatus
{
    Temporary,
    Effective,
}

public class RoadAR : AggregateBase
{
    public string? ExternalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public RoadStatus Status { get; private set; }
    public bool Deleted { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }

    public RoadAR()
    {
        Register<RoadCreated>(Apply);
        Register<RoadUpdated>(Apply);
        Register<RoadDeleted>(Apply);
    }

    public Result Create(
        Guid id,
        string? externalId,
        string name,
        RoadStatus status,
        DateTime created,
        DateTime updated)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(id)} cannot be empty guid."));
        }

        if (String.IsNullOrWhiteSpace(externalId))
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.EXTERNAL_ID_CANNOT_BE_WHITE_SPACE_OR_NULL,
                    $"{nameof(externalId)} is not allowed to be whitespace or null."));
        }

        if (created == default)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.CREATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(created)} being default date is invalid."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        RaiseEvent(new RoadCreated(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            created: created,
            updated: updated));

        return Result.Ok();
    }

    public Result Update(
        string name,
        string externalId,
        RoadStatus status,
        DateTime updated)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.CANNOT_UPDATE_DELETED,
                    @$"Cannot update deleted road with id: '{Id}'."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        var hasChanges = () =>
        {
            if (Name != name)
            {
                return true;
            }
            if (ExternalId != externalId)
            {
                return true;
            }
            if (Status != status)
            {
                return true;
            }

            return false;
        };

        if (!hasChanges())
        {
            return Result.Fail(
               new RoadError(
                   RoadErrorCode.NO_CHANGES,
                   $"No changes for road with id '{Id}'."));
        }

        RaiseEvent(new RoadUpdated(
            id: Id,
            externalId: externalId,
            name: name,
            status: status,
            updated: updated));

        return Result.Ok();
    }

    public Result Delete(DateTime updated)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.CANNOT_DELETE_ALREADY_DELETED,
                    $"Id: '{Id}' is already deleted."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        RaiseEvent(new RoadDeleted(Id, updated));

        return Result.Ok();
    }

    private void Apply(RoadCreated roadCreated)
    {
        Id = roadCreated.Id;
        ExternalId = roadCreated.ExternalId;
        Name = roadCreated.Name;
        Status = roadCreated.Status;
        Created = roadCreated.Created;
        Updated = roadCreated.Updated;
    }

    private void Apply(RoadUpdated roadUpdated)
    {
        ExternalId = roadUpdated.ExternalId;
        Name = roadUpdated.Name;
        Status = roadUpdated.Status;
        Updated = roadUpdated.Updated;
    }

    private void Apply(RoadDeleted roadDeleted)
    {
        Deleted = true;
        Updated = roadDeleted.Updated;
    }
}
