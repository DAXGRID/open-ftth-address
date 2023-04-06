namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressFloorNameChanged
{
    public Guid Id { get; init; }
    public string? FloorName { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressFloorNameChanged(
        Guid id,
        string? floorName,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        FloorName = floorName;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
