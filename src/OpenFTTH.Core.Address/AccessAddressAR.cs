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
    public DateTime? LocationUpdated { get; private set; }
    public string? SupplementaryTownName { get; private set; }
    public string? PlotId { get; private set; }
    public Guid RoadId { get; private set; }

    public AccessAddressAR()
    {
        Register<AccessAddressCreated>(Apply);
        Register<AccessAddressUpdated>(Apply);
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
        DateTime? locationUpdated,
        string? supplementaryTownName,
        string? plotId,
        Guid roadId,
        HashSet<Guid> existingRoadIds,
        HashSet<Guid> existingPostCodeIds)
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
                locationUpdated: locationUpdated,
                townName: supplementaryTownName,
                plotId: plotId,
                roadId: roadId));

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
        DateTime? locationUpdated,
        string? supplementaryTownName,
        string? plotId,
        Guid roadId,
        HashSet<Guid> existingRoadIds,
        HashSet<Guid> existingPostCodeIds)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
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
                locationUpdated: locationUpdated,
                townName: supplementaryTownName,
                plotId: plotId,
                roadId: roadId));

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
        LocationUpdated = accessAddressCreated.LocationUpdated;
        SupplementaryTownName = accessAddressCreated.TownName;
        PlotId = accessAddressCreated.PlotId;
        RoadId = accessAddressCreated.RoadId;
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
        LocationUpdated = accessAddressUpdated.LocationUpdated;
        SupplementaryTownName = accessAddressUpdated.TownName;
        PlotId = accessAddressUpdated.PlotId;
        RoadId = accessAddressUpdated.RoadId;
    }
}
