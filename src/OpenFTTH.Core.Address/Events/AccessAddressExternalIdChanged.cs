namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressExternalIdChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string? ExternalId { get; init; }

    public AccessAddressExternalIdChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string? externalId)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        ExternalId = externalId;
    }
}
