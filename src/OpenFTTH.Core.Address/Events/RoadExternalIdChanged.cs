namespace OpenFTTH.Core.Address.Events;

public sealed record RoadExternalIdChanged
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public RoadExternalIdChanged(
        Guid id,
        string externalId,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalId = externalId;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
