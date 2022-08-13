using FluentResults;
using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public enum UnitAddressStatus
{
    Active,
    Canceled,
    Pending,
    Discontinued
}

public class UnitAddressAR : AggregateBase
{
    public string? OfficialId { get; private set; }
    public Guid AccessAddressId { get; private set; }
    public UnitAddressStatus Status { get; private set; }
    public string? FloorName { get; private set; }
    public string? SuitName { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public bool Deleted { get; private set; }
    public bool PendingOfficial { get; private set; }

    public UnitAddressAR()
    {
        Register<UnitAddressCreated>(Apply);
        Register<UnitAddressUpdated>(Apply);
        Register<UnitAddressDeleted>(Apply);
    }

    public Result Create(
        Guid id,
        string? officialId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime updated,
        HashSet<Guid> existingAccessAddressIds,
        bool pendingOfficial)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    "{nameof(id)} cannot be empty guid."));
        }

        if (accessAddressId == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(accessAddressId)} cannot be empty guid."));
        }

        if (created == default)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(created)} being default date is invalid."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        if (existingAccessAddressIds is null ||
            !existingAccessAddressIds.Contains(accessAddressId))
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ACCESS_ADDRESS_DOES_NOT_EXISTS,
                    @$" Cannot reference to a access-address
that does not exist ('{accessAddressId}')."));
        }

        RaiseEvent(new UnitAddressCreated(
                       id: id,
                       officialId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       created: created,
                       updated: updated,
                       pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Update(
        string? officialId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime updated,
        HashSet<Guid> existingAccessAddressIds,
        bool pendingOfficial)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ID_NOT_SET,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.CANNOT_UPDATE_DELETED,
                    @$"Cannot update when deleted."));
        }

        if (accessAddressId == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(accessAddressId)} cannot be empty guid."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        if (existingAccessAddressIds is null ||
            !existingAccessAddressIds.Contains(accessAddressId))
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ACCESS_ADDRESS_DOES_NOT_EXISTS,
                    @$" Cannot reference to a access-address
that does not exist ('{accessAddressId}')."));
        }

        var hasChanges = () =>
        {
            if (OfficialId != officialId)
            {
                return true;
            }
            if (AccessAddressId != accessAddressId)
            {
                return true;
            }
            if (Status != status)
            {
                return true;
            }
            if (FloorName != floorName)
            {
                return true;
            }
            if (SuitName != suitName)
            {
                return true;
            }
            if (PendingOfficial != pendingOfficial)
            {
                return true;
            }

            return false;
        };

        if (!hasChanges())
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.NO_CHANGES,
                    @$" Cannot reference to a access-address
that does not exist ('{accessAddressId}')."));
        }

        RaiseEvent(new UnitAddressUpdated(
                       id: Id,
                       officialAddressId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       updated: updated,
                       pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Delete(DateTime updated)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ID_NOT_SET,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.CANNOT_DELETE_ALREADY_DELETED,
                    @$"Cannot delete already deleted."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)} being default date is invalid."));
        }

        RaiseEvent(new UnitAddressDeleted(Id, updated));

        return Result.Ok();
    }

    private void Apply(UnitAddressCreated unitAddressCreated)
    {
        Id = unitAddressCreated.Id;
        OfficialId = unitAddressCreated.OfficialId;
        AccessAddressId = unitAddressCreated.AccessAddressId;
        Status = unitAddressCreated.Status;
        FloorName = unitAddressCreated.FloorName;
        SuitName = unitAddressCreated.SuitName;
        Created = unitAddressCreated.Created;
        Updated = unitAddressCreated.Updated;
        PendingOfficial = unitAddressCreated.PendingOfficial;
    }

    private void Apply(UnitAddressUpdated unitAddressCreated)
    {
        OfficialId = unitAddressCreated.OfficialId;
        AccessAddressId = unitAddressCreated.AccessAddressId;
        Status = unitAddressCreated.Status;
        FloorName = unitAddressCreated.FloorName;
        SuitName = unitAddressCreated.SuitName;
        Updated = unitAddressCreated.Updated;
        PendingOfficial = unitAddressCreated.PendingOfficial;
    }

    private void Apply(UnitAddressDeleted unitAddressDeleted)
    {
        Deleted = true;
        Updated = unitAddressDeleted.Updated;
    }
}
