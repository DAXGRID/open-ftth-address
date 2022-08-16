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
    public string? ExternalId { get; private set; }
    public Guid AccessAddressId { get; private set; }
    public UnitAddressStatus Status { get; private set; }
    public string? FloorName { get; private set; }
    public string? SuitName { get; private set; }
    public DateTime? ExternalCreatedDate { get; private set; }
    public DateTime? ExternalUpdatedDate { get; private set; }
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
        string? externalId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate,
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
                       externalId: externalId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       externalCreatedDate: externalCreatedDate,
                       externalUpdatedDate: externalUpdatedDate,
                       pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Update(
        string? externalId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime? externalUpdatedDate,
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
            if (ExternalId != externalId)
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
                       officialAddressId: externalId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       externalUpdatedDate: externalUpdatedDate,
                       pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Delete(DateTime? updated)
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

        RaiseEvent(new UnitAddressDeleted(Id, updated));

        return Result.Ok();
    }

    private void Apply(UnitAddressCreated unitAddressCreated)
    {
        Id = unitAddressCreated.Id;
        ExternalId = unitAddressCreated.ExternalId;
        AccessAddressId = unitAddressCreated.AccessAddressId;
        Status = unitAddressCreated.Status;
        FloorName = unitAddressCreated.FloorName;
        SuitName = unitAddressCreated.SuitName;
        ExternalCreatedDate = unitAddressCreated.ExternalCreatedDate;
        ExternalUpdatedDate = unitAddressCreated.ExternalUpdatedDate;
        PendingOfficial = unitAddressCreated.PendingOfficial;
    }

    private void Apply(UnitAddressUpdated unitAddressCreated)
    {
        ExternalId = unitAddressCreated.ExternalId;
        AccessAddressId = unitAddressCreated.AccessAddressId;
        Status = unitAddressCreated.Status;
        FloorName = unitAddressCreated.FloorName;
        SuitName = unitAddressCreated.SuitName;
        ExternalUpdatedDate = unitAddressCreated.ExternalUpdatedDate;
        PendingOfficial = unitAddressCreated.PendingOfficial;
    }

    private void Apply(UnitAddressDeleted unitAddressDeleted)
    {
        Deleted = true;
        ExternalUpdatedDate = unitAddressDeleted.ExternalUpdatedDate;
    }
}
