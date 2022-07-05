using FluentResults;
using OpenFTTH.Address.Business.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class AccessAddressAR : AggregateBase
{
    public Guid? OfficialId { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public string? MunicipalCode { get; private set; }
    public Status Status { get; private set; }
    public string? RoadCode { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? PostDistrictCode { get; private set; }
    public double EastCoordinate { get; private set; }
    public double NorthCoordinate { get; private set; }
    public DateTime? LocationUpdated { get; private set; }
    public string? TownName { get; private set; }
    public string? PlotId { get; private set; }
    public Guid RoadId { get; private set; }

    public AccessAddressAR()
    {
        Register<AccessAddressCreated>(Apply);
        Register<AccessAddressUpdated>(Apply);
    }

    public Result Create(
        Guid id,
        Guid? officialId,
        DateTime created,
        DateTime updated,
        string municipalCode,
        Status status,
        string roadCode,
        string houseNumber,
        string postDistrictCode,
        double eastCoordinate,
        double northCoordinate,
        DateTime? locationUpdated,
        string? townName,
        string? plotId,
        Guid roadId)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(id)} cannot be empty guid."));
        }

        if (updated == new DateTime())
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)}, being default date, is invalid."));
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
                postDistrictCode: postDistrictCode,
                eastCoordinate: eastCoordinate,
                northCoordinate: northCoordinate,
                locationUpdated: locationUpdated,
                townName: townName,
                plotId: plotId,
                roadId: roadId));

        return Result.Ok();
    }

    public Result Update(
        Guid? officialId,
        DateTime updated,
        string municipalCode,
        Status status,
        string roadCode,
        string houseNumber,
        string postDistrictCode,
        double eastCoordinate,
        double northCoordinate,
        DateTime? locationUpdated,
        string? townName,
        string? plotId,
        Guid roadId)
    {
        if (updated == new DateTime())
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.UPDATED_CANNOT_BE_DEFAULT_DATE,
                    $"{nameof(updated)}, being default date, is invalid."));
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
                postDistrictCode: postDistrictCode,
                eastCoordinate: eastCoordinate,
                northCoordinate: northCoordinate,
                locationUpdated: locationUpdated,
                townName: townName,
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
        PostDistrictCode = accessAddressCreated.PostDistrictCode;
        EastCoordinate = accessAddressCreated.EastCoordinate;
        NorthCoordinate = accessAddressCreated.NorthCoordinate;
        LocationUpdated = accessAddressCreated.LocationUpdated;
        TownName = accessAddressCreated.TownName;
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
        PostDistrictCode = accessAddressUpdated.PostDistrictCode;
        EastCoordinate = accessAddressUpdated.EastCoordinate;
        NorthCoordinate = accessAddressUpdated.NorthCoordinate;
        LocationUpdated = accessAddressUpdated.LocationUpdated;
        TownName = accessAddressUpdated.TownName;
        PlotId = accessAddressUpdated.PlotId;
        RoadId = accessAddressUpdated.RoadId;
    }
}
