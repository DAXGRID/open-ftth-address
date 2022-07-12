namespace OpenFTTH.Core.Address.Events;

public record PostCodeCreated
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public string Name { get; init; }

    public PostCodeCreated(Guid id, string number, string name)
    {
        Id = id;
        Number = number;
        Name = name;
    }
}
