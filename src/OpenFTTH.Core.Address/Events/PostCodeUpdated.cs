namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public PostCodeUpdated(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
