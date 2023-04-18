namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressRoadCodeChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string RoadCode { get; init; }

    public AccessAddressRoadCodeChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string roadCode)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        RoadCode = roadCode;
    }
}
