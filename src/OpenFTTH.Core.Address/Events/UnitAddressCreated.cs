namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressCreated
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public Guid AccessAddressId { get; init; }
    public UnitAddressStatus Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuiteName { get; init; }
    public DateTime? ExternalCreatedDate { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public bool PendingOfficial { get; init; }

    public UnitAddressCreated(
        Guid id,
        string? externalId,
        Guid accessAddressId,
        UnitAddressStatus status,
        string? floorName,
        string? suiteName,
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate,
        bool pendingOfficial)
    {
        Id = id;
        ExternalId = externalId;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuiteName = suiteName;
        ExternalCreatedDate = externalCreatedDate;
        ExternalUpdatedDate = externalUpdatedDate;
        PendingOfficial = pendingOfficial;
    }
}
