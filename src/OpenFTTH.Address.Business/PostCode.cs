namespace OpenFTTH.Address.Business;

public record PostCode
{
    public string Number { get; init; }
    public string Name { get; init; }

    public PostCode(string number, string name)
    {
        Number = number;
        Name = name;
    }
}
