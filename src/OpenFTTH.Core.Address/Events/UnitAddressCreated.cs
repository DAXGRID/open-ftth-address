namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressCreated
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public Guid AccessAddressId { get; init; }
    public UnitAddressStatus Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public bool PendingOfficial { get; init; }

    public UnitAddressCreated(
        Guid id,
        string? externalId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime updated,
        bool pendingOfficial)
    {
        Id = id;
        ExternalId = externalId;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuitName = suitName;
        Created = created;
        Updated = updated;
        PendingOfficial = pendingOfficial;
    }
}
