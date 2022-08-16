namespace OpenFTTH.Core.Address.Events;

public sealed record PostCodeDeleted
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public PostCodeDeleted(Guid id, DateTime? externalUpdatedDate)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
