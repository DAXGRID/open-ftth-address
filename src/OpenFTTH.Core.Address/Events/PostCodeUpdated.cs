namespace OpenFTTH.Core.Address.Events;

public record PostCodeUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public PostCodeUpdated(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
