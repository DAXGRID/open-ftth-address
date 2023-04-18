namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeNameChanged
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public PostCodeNameChanged(Guid id, string name, DateTime? externalUpdatedDate)
    {
        Id = id;
        Name = name;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
