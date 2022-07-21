namespace OpenFTTH.Core.Address;

public sealed record RoadDeleted
{
    public Guid Id { get; init; }

    public RoadDeleted(Guid id)
    {
        Id = id;
    }
}
