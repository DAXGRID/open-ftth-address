namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime Updated { get; init; }

    public PostCodeUpdated(Guid id, string name, DateTime updated)
    {
        Id = id;
        Name = name;
        Updated = updated;
    }
}
