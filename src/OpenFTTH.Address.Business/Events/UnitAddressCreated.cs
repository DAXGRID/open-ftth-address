namespace OpenFTTH.Address.Business.Events;

public record UnitAddressCreated
{
    public Guid Id { get; init; }
    public Guid? OfficialId { get; init; }
    public Guid AccessAddressId { get; init; }
    public Status Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public UnitAddressCreated(
        Guid id,
        Guid? officialId,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime updated)
    {
        Id = id;
        OfficialId = officialId;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuitName = suitName;
        Created = created;
        Updated = updated;
    }
}
