namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeCreated
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public string Name { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public PostCodeCreated(
        Guid id,
        string number,
        string name,
        DateTime created,
        DateTime updated)
    {
        Id = id;
        Number = number;
        Name = name;
        Created = created;
        Updated = updated;
    }
}
