namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeCreated
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public string Name { get; init; }
    public DateTime? ExternalCreatedDate { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public PostCodeCreated(
        Guid id,
        string number,
        string name,
        DateTime? externalCreatedDate,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        Number = number;
        Name = name;
        ExternalCreatedDate = externalCreatedDate;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
