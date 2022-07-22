namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressUpdated
{
    public Guid Id { get; init; }
    public string? OfficialId { get; init; }
    public Guid AccessAddressId { get; init; }
    public AddressStatus Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Updated { get; init; }

    public UnitAddressUpdated(
        Guid id,
        string? officialAddressId,
        Guid accessAddressId,
        AddressStatus status,
        string? floorName,
        string? suitName,
        DateTime updated)
    {
        Id = id;
        OfficialId = officialAddressId;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuitName = suitName;
        Updated = updated;
    }
}
