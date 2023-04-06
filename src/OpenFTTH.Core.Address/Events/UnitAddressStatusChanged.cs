namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressStatusChanged
{
    public Guid Id { get; init; }
    public UnitAddressStatus Status { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressStatusChanged(
        Guid id,
        UnitAddressStatus status,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        Status = status;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
