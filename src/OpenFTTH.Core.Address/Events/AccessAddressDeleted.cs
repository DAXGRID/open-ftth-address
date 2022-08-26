namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressDeleted
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public AccessAddressDeleted(Guid id, DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
