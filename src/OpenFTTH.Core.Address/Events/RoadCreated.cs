namespace OpenFTTH.Core.Address.Events;

public sealed record RoadCreated
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public RoadCreated(
        Guid id,
        string externalId,
        string name,
        RoadStatus status,
        DateTime created,
        DateTime updated)
    {
        Id = id;
        ExternalId = externalId;
        Name = name;
        Status = status;
        Created = created;
        Updated = updated;
    }
}
