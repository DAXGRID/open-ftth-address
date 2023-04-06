namespace OpenFTTH.Core.Address.Events;

public sealed record RoadStatusChanged
{
    public Guid Id { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public RoadStatusChanged(Guid id, RoadStatus status, DateTime? externalUpdatedDate)
    {
        Id = id;
        Status = status;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
