namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeDeleted
{
    public Guid Id { get; init; }
    public DateTime Updated { get; init; }

    public PostCodeDeleted(Guid id, DateTime updated)
    {
        Id = id;
        Updated = updated;
    }
}
