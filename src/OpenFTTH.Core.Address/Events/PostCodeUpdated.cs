namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeUpdated
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public PostCodeUpdated(Guid id, string name, DateTime? externalUpdatedDate)
    {
        Id = id;
        Name = name;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
