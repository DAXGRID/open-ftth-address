namespace OpenFTTH.Core.Address.Events;

public record AccessAddressUpdated
{
    public Guid Id { get; init; }
    public string? OfficialId { get; init; }
    public DateTime Updated { get; init; }
    public string MunicipalCode { get; init; }
    public Status Status { get; init; }
    public string RoadCode { get; init; }
    public string HouseNumber { get; init; }
    public Guid PostCodeId { get; init; }
    public double EastCoordinate { get; init; }
    public double NorthCoordinate { get; init; }
    public DateTime? LocationUpdated { get; init; }
    public string? TownName { get; init; }
    public string? PlotId { get; init; }
    public Guid RoadId { get; init; }

    public AccessAddressUpdated(
        Guid id,
        string? officialId,
        DateTime updated,
        string municipalCode,
        Status status,
        string roadCode,
        string houseNumber,
        Guid postCodeId,
        double eastCoordinate,
        double northCoordinate,
        DateTime? locationUpdated,
        string? townName,
        string? plotId,
        Guid roadId)
    {
        Id = id;
        OfficialId = officialId;
        Updated = updated;
        MunicipalCode = municipalCode;
        Status = status;
        RoadCode = roadCode;
        HouseNumber = houseNumber;
        PostCodeId = postCodeId;
        EastCoordinate = eastCoordinate;
        NorthCoordinate = northCoordinate;
        LocationUpdated = locationUpdated;
        TownName = townName;
        PlotId = plotId;
        RoadId = roadId;
    }
}
