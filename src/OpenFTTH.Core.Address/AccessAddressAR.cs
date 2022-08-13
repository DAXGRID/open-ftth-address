using FluentResults;
using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public enum AccessAddressStatus
{
    Active,
    Canceled,
    Pending,
    Discontinued
}

public class AccessAddressAR : AggregateBase
{
    public string? OfficialId { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public string? MunicipalCode { get; private set; }
    public AccessAddressStatus Status { get; private set; }
    public string? RoadCode { get; private set; }
    public string? HouseNumber { get; private set; }
    public Guid PostCodeId { get; private set; }
    public double EastCoordinate { get; private set; }
    public double NorthCoordinate { get; private set; }
    public string? SupplementaryTownName { get; private set; }
    public string? PlotId { get; private set; }
    public Guid RoadId { get; private set; }
    public bool Deleted { get; private set; }
    public bool PendingOfficial { get; private set; }

    public AccessAddressAR()
    {
        Register<AccessAddressCreated>(Apply);
        Register<AccessAddressUpdated>(Apply);
        Register<AccessAddressDeleted>(Apply);
    }

    public Result Create(
        Guid id,
        string? officialId,
        DateTime created,
        DateTime updated,
        string municipalCode,
        AccessAddressStatus status,
        string roadCode,
        string houseNumber,
        Guid postCodeId,
        double eastCoordinate,
        double northCoordinate,
        string? supplementaryTownName,
        string? plotId,
        Guid roadId,
        HashSet<Guid> existingRoadIds,
        HashSet<Guid> existingPostCodeIds,
        bool pendingOfficial)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(id)} cannot be empty guid."));
        }

        if (created == default)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.CREATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(created)}, being default date, is invalid."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)}, being default date, is invalid."));
        }

        if (existingRoadIds is null || !existingRoadIds.Contains(roadId))
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ROAD_DOES_NOT_EXIST,
                    $"No roads exist with id '{roadId}'."));
        }

        if (existingPostCodeIds is null || !existingPostCodeIds.Contains(postCodeId))
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.POST_CODE_DOES_NOT_EXIST,
                    $"No postcode wxists with id '{postCodeId}'."));
        }

        Id = id;

        RaiseEvent(
            new AccessAddressCreated(
                id: id,
                officialId: officialId,
                created: created,
                updated: updated,
                municipalCode: municipalCode,
                status: status,
                roadCode: roadCode,
                houseNumber: houseNumber,
                postCodeId: postCodeId,
                eastCoordinate: eastCoordinate,
                northCoordinate: northCoordinate,
                townName: supplementaryTownName,
                plotId: plotId,
                roadId: roadId, pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Update(
        string? officialId,
        DateTime updated,
        string municipalCode,
        AccessAddressStatus status,
        string roadCode,
        string houseNumber,
        Guid postCodeId,
        double eastCoordinate,
        double northCoordinate,
        string? supplementaryTownName,
        string? plotId,
        Guid roadId,
        HashSet<Guid> existingRoadIds,
        HashSet<Guid> existingPostCodeIds,
        bool pendingOfficial)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.CANNOT_UPDATE_DELETED,
                    @$"{nameof(Id)}, is deleted, cannot be updated."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)}, being default date, is invalid."));
        }

        if (existingRoadIds is null || !existingRoadIds.Contains(roadId))
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ROAD_DOES_NOT_EXIST,
                    @$"No roads exist with id '{roadId}'."));
        }

        if (existingPostCodeIds is null || !existingPostCodeIds.Contains(postCodeId))
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.POST_CODE_DOES_NOT_EXIST,
                    $"No postcode exists with id '{postCodeId}'."));
        }

        var hasChanges = () =>
        {
            if (OfficialId != officialId)
            {
                return true;
            }
            if (MunicipalCode != municipalCode)
            {
                return true;
            }
            if (RoadCode != roadCode)
            {
                return true;
            }
            if (HouseNumber != houseNumber)
            {
                return true;
            }
            if (PostCodeId != postCodeId)
            {
                return true;
            }
            if (EastCoordinate != eastCoordinate)
            {
                return true;
            }
            if (NorthCoordinate != northCoordinate)
            {
                return true;
            }
            if (SupplementaryTownName != supplementaryTownName)
            {
                return true;
            }
            if (PlotId != plotId)
            {
                return true;
            }
            if (RoadId != roadId)
            {
                return true;
            }

            return false;
        };

        if (!hasChanges())
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.NO_CHANGES,
                    $"Cannot update access address, no changes for id '{Id}'."));
        }

        RaiseEvent(
            new AccessAddressUpdated(
                id: Id,
                officialId: officialId,
                updated: updated,
                municipalCode: municipalCode,
                status: status,
                roadCode: roadCode,
                houseNumber: houseNumber,
                postCodeId: postCodeId,
                eastCoordinate: eastCoordinate,
                northCoordinate: northCoordinate,
                townName: supplementaryTownName,
                plotId: plotId,
                roadId: roadId,
                pendingOfficial: pendingOfficial));

        return Result.Ok();
    }

    public Result Delete(DateTime updated)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        if (Deleted)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.CANNOT_DELETE_ALREADY_DELETED,
                    @$"{nameof(Id)}, is already deleted."));
        }

        if (updated == default)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)}, being default date, is invalid."));
        }

        RaiseEvent(new AccessAddressDeleted(Id, updated));

        return Result.Ok();
    }

    private void Apply(AccessAddressCreated accessAddressCreated)
    {
        Id = accessAddressCreated.Id;
        OfficialId = accessAddressCreated.OfficialId;
        Created = accessAddressCreated.Created;
        Updated = accessAddressCreated.Updated;
        MunicipalCode = accessAddressCreated.MunicipalCode;
        Status = accessAddressCreated.Status;
        RoadCode = accessAddressCreated.RoadCode;
        HouseNumber = accessAddressCreated.HouseNumber;
        PostCodeId = accessAddressCreated.PostCodeId;
        EastCoordinate = accessAddressCreated.EastCoordinate;
        NorthCoordinate = accessAddressCreated.NorthCoordinate;
        SupplementaryTownName = accessAddressCreated.TownName;
        PlotId = accessAddressCreated.PlotId;
        RoadId = accessAddressCreated.RoadId;
        PendingOfficial = accessAddressCreated.PendingOfficial;
    }

    private void Apply(AccessAddressUpdated accessAddressUpdated)
    {
        OfficialId = accessAddressUpdated.OfficialId;
        Updated = accessAddressUpdated.Updated;
        MunicipalCode = accessAddressUpdated.MunicipalCode;
        Status = accessAddressUpdated.Status;
        RoadCode = accessAddressUpdated.RoadCode;
        HouseNumber = accessAddressUpdated.HouseNumber;
        PostCodeId = accessAddressUpdated.PostCodeId;
        EastCoordinate = accessAddressUpdated.EastCoordinate;
        NorthCoordinate = accessAddressUpdated.NorthCoordinate;
        SupplementaryTownName = accessAddressUpdated.TownName;
        PlotId = accessAddressUpdated.PlotId;
        RoadId = accessAddressUpdated.RoadId;
        PendingOfficial = accessAddressUpdated.PendingOfficial;
    }

    private void Apply(AccessAddressDeleted accessAddressDeleted)
    {
        Deleted = true;
        Updated = accessAddressDeleted.Updated;
    }
}
