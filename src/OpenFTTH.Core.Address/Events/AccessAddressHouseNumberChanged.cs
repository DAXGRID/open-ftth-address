namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressHouseNumberChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string HouseNumber { get; init; }

    public AccessAddressHouseNumberChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string houseNumber)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        HouseNumber = houseNumber;
    }
}
