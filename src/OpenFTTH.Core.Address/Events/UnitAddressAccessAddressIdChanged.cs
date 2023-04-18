namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressAccessAddressIdChanged
{
    public Guid Id { get; init; }
    public Guid AccessAddressId { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressAccessAddressIdChanged(
        Guid id,
        Guid accessAddressId,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        AccessAddressId = accessAddressId;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
