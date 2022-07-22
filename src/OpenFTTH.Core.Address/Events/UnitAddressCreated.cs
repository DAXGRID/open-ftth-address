namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressCreated
{
    public Guid Id { get; init; }
    public string? OfficialId { get; init; }
    public Guid AccessAddressId { get; init; }
    public AddressStatus Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public UnitAddressCreated(
        Guid id,
        string? officialId,
        Guid accessAddressId,
        AddressStatus status,
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
