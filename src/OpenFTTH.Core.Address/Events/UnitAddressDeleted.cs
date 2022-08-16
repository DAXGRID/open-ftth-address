namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressDeleted
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressDeleted(Guid id, DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
