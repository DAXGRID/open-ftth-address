namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressStatusChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public AccessAddressStatus Status { get; init; }

    public AccessAddressStatusChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        AccessAddressStatus status)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        Status = status;
    }
}
