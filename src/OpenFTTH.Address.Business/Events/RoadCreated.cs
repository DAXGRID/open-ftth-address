namespace OpenFTTH.Address.Business.Events;

public record RoadCreated
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }

    public RoadCreated(Guid id, string externalId, string name)
    {
        Id = id;
        ExternalId = externalId;
        Name = name;
    }
}
