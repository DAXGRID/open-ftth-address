namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressPendingOfficialChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public bool PendingOfficial { get; init; }

    public AccessAddressPendingOfficialChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        bool pendingOfficial)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        PendingOfficial = pendingOfficial;
    }
}
