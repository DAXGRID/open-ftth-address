namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressCoordinateChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public double EastCoordinate { get; init; }
    public double NorthCoordinate { get; init; }

    public AccessAddressCoordinateChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        double eastCoordinate,
        double northCoordinate)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        EastCoordinate = eastCoordinate;
        NorthCoordinate = northCoordinate;
    }
}
