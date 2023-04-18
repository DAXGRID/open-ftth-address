namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressExternalIdChanged
{
    public Guid Id { get; init; }
    public string? ExternalId { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressExternalIdChanged(
        Guid id,
        string? externalId,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalId = externalId;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
