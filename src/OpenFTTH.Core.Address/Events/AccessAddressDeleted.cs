namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressDeleted
{
    public Guid Id { get; init; }
    public DateTime Updated { get; init; }

    public AccessAddressDeleted(Guid id, DateTime updated)
    {
        Id = id;
        Updated = updated;
    }
}
