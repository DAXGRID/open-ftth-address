namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressDeleted
{
    public Guid Id { get; init; }

    public UnitAddressDeleted(Guid id)
    {
        Id = id;
    }
}
