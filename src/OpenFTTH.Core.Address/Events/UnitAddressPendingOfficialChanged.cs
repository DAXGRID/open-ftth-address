namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressPendingOfficialChanged
{
    public Guid Id { get; init; }
    public bool PendingOfficial { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressPendingOfficialChanged(
        Guid id,
        bool pendingOfficial,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        PendingOfficial = pendingOfficial;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
