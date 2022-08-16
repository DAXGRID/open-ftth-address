namespace OpenFTTH.Core.Address.Events;

public sealed record RoadCreated
{
    public Guid Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public RoadStatus Status { get; init; }
    public DateTime? ExternalCreatedDate { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public RoadCreated(
        Guid id,
        string externalId,
        string name,
        RoadStatus status,
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalId = externalId;
        Name = name;
        Status = status;
        ExternalCreatedDate = externalCreatedDate;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
