namespace OpenFTTH.Core.Address.Events;

public sealed record RoadUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }

    public RoadUpdated(Guid id, string name, RoadStatus status)
    {
        Id = id;
        Name = name;
        Status = status;
    }
}
