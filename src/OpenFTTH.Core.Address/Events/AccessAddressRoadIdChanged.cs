namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressRoadIdChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public Guid RoadId { get; init; }

    public AccessAddressRoadIdChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        Guid roadId)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        RoadId = roadId;
    }
}
