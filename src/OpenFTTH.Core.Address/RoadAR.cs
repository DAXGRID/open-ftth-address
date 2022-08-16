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
    public DateTime? ExternalCreatedDate { get; private set; }
    public DateTime? ExternalUpdatedDate { get; private set; }

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
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate)
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

        RaiseEvent(new RoadCreated(
            id: id,
            externalId: externalId,
            name: name,
            status: status,
            externalCreatedDate: externalCreatedDate,
            externalUpdatedDate: externalUpdatedDate));

        return Result.Ok();
    }

    public Result Update(
        string name,
        string externalId,
        RoadStatus status,
        DateTime? externalUpdatedDate)
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
            externalUpdatedDate: externalUpdatedDate));

        return Result.Ok();
    }

    public Result Delete(DateTime? externalUpdatedDate)
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

        RaiseEvent(new RoadDeleted(Id, externalUpdatedDate));

        return Result.Ok();
    }

    private void Apply(RoadCreated roadCreated)
    {
        Id = roadCreated.Id;
        ExternalId = roadCreated.ExternalId;
        Name = roadCreated.Name;
        Status = roadCreated.Status;
        ExternalCreatedDate = roadCreated.ExternalCreatedDate;
        ExternalUpdatedDate = roadCreated.ExternalUpdatedDate;
    }

    private void Apply(RoadUpdated roadUpdated)
    {
        ExternalId = roadUpdated.ExternalId;
        Name = roadUpdated.Name;
        Status = roadUpdated.Status;
        ExternalUpdatedDate = roadUpdated.ExternalUpdatedDate;
    }

    private void Apply(RoadDeleted roadDeleted)
    {
        Deleted = true;
        ExternalUpdatedDate = roadDeleted.ExternalUpdatedDate;
    }
}
