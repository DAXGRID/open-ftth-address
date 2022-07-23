namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressDeleted
{
    public Guid Id { get; init; }
    public AccessAddressStatus Status { get; init; }

    public AccessAddressDeleted(Guid id, AccessAddressStatus status)
    {
        Id = id;
        Status = status;
    }
}
