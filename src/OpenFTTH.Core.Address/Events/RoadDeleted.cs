namespace OpenFTTH.Core.Address.Events;

public sealed record RoadDeleted
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public RoadDeleted(Guid id, DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
