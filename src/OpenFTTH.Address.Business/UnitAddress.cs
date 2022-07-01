namespace OpenFTTH.Address.Business;

public record UnitAddress
{
    public Guid Id { get; init; }
    public Guid AccessAddressId { get; init; }
    public Guid? OfficialId { get; init; }
    public Status Status { get; init; }
    public string? FloorName { get; init; }
    public string? SuitName { get; init; }
    public DateTime Created { get; init; }
    public DateTime? Updated { get; init; }

    public UnitAddress(
        Guid id,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime? updated)
    {
        Id = id;
        AccessAddressId = accessAddressId;
        Status = status;
        FloorName = floorName;
        SuitName = suitName;
        Created = created;
        Updated = updated;
    }
}
