namespace OpenFTTH.Core.Address.Events;

public sealed record RoadCreated
{
    public Guid Id { get; init; }
    public string OfficialId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }

    public RoadCreated(
        Guid id,
        string officialId,
        string name,
        RoadStatus status)
    {
        Id = id;
        OfficialId = officialId;
        Name = name;
        Status = status;
    }
}
