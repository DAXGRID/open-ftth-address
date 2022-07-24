namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressDeleted
{
    public Guid Id { get; init; }

    public AccessAddressDeleted(Guid id)
    {
        Id = id;
    }
}
