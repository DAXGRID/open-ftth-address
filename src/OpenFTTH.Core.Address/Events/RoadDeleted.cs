namespace OpenFTTH.Core.Address.Events;

public sealed record RoadDeleted
{
    public Guid Id { get; init; }
    public DateTime Updated { get; init; }

    public RoadDeleted(Guid id, DateTime updated)
    {
        Id = id;
        Updated = updated;
    }
}
