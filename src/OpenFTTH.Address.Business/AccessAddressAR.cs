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
        if (officialId == Guid.Empty)
        {
            return Result.Fail(
                new AccessAddressError(
                    AccessAddressErrorCodes.ID_CANNOT_BE_NULL_OR_EMPTY,
                    "Invalid work project id. Cannot be null or empty."));
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
}
