using FluentResults;
using OpenFTTH.Address.Business.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class UnitAddressAR : AggregateBase
{
    public Guid? OfficialId { get; private set; }
    public Guid AccessAddressId { get; private set; }
    public Status Status { get; private set; }
    public string? FloorName { get; private set; }
    public string? SuitName { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }

    public UnitAddressAR()
    {
        Register<UnitAddressCreated>(Apply);
        Register<UnitAddressUpdated>(Apply);
    }

    public Result Create(
        Guid id,
        Guid? officialId,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime updated,
        HashSet<Guid> accessAddressIds)
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

        if (accessAddressIds is null || !accessAddressIds.Contains(accessAddressId))
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ACCESS_ADDRESS_DOES_NOT_EXISTS,
                    @$" Cannot reference to a {nameof(AccessAddress)} that does not exists ('{accessAddressId}')."));
        }

        Id = id;

        RaiseEvent(new UnitAddressCreated(
                       id: id,
                       officialId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       created: created,
                       updated: updated));

        return Result.Ok();
    }

    public Result Update(
        Guid? officialId,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime updated)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new UnitAddressError(
                    UnitAddressErrorCodes.ID_NOT_SET,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
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

        RaiseEvent(new UnitAddressUpdated(
                       id: Id,
                       officialAddressId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       updated: updated));

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
    }

    private void Apply(UnitAddressUpdated unitAddressCreated)
    {
        OfficialId = unitAddressCreated.OfficialId;
        AccessAddressId = unitAddressCreated.AccessAddressId;
        Status = unitAddressCreated.Status;
        FloorName = unitAddressCreated.FloorName;
        SuitName = unitAddressCreated.SuitName;
        Updated = unitAddressCreated.Updated;
    }
}
