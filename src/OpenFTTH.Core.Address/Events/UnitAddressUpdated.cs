namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressUpdated
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public Guid AccessAddressId { get; init; }
    public UnitAddressStatus Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Updated { get; init; }
    public bool PendingOfficial { get; init; }

    public UnitAddressUpdated(
        Guid id,
        string? officialAddressId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime updated,
        bool pendingOfficial)
    {
        Id = id;
        ExternalId = officialAddressId;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuitName = suitName;
        Updated = updated;
        PendingOfficial = pendingOfficial;
    }
}
