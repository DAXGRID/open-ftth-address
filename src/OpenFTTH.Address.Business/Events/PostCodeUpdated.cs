namespace OpenFTTH.Address.Business;

public record PostCodeUpdated
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public string Name { get; init; }

    public PostCodeUpdated(Guid id, string number, string name)
    {
        Id = id;
        Number = number;
        Name = name;
    }
}
