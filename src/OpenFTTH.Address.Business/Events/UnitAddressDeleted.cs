namespace OpenFTTH.Address.Business.Events;

public record UnitAddressDeleted
{
    public Guid Id { get; init; }
    public DateTime Updated { get; init; }

    public UnitAddressDeleted(
        Guid id,
        DateTime updated)
    {
        Id = id;
        Updated = updated;
    }
}
