namespace OpenFTTH.Core.Address.Events;

public sealed record RoadUpdated
{
    public Guid Id { get; init; }
    public string OfficialId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime Updated { get; init; }

    public RoadUpdated(
        Guid id,
        string officialId,
        string name,
        RoadStatus status,
        DateTime updated)
    {
        Id = id;
        OfficialId = officialId;
        Name = name;
        Status = status;
        Updated = updated;
    }
}
