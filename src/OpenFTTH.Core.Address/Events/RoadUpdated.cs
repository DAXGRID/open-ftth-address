namespace OpenFTTH.Core.Address.Events;

public sealed record RoadUpdated
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime Updated { get; init; }

    public RoadUpdated(
        Guid id,
        string externalId,
        string name,
        RoadStatus status,
        DateTime updated)
    {
        Id = id;
        ExternalId = externalId;
        Name = name;
        Status = status;
        Updated = updated;
    }
}
