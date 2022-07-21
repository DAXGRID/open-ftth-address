namespace OpenFTTH.Core.Address.Events;

public sealed record RoadUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public RoadUpdated(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
