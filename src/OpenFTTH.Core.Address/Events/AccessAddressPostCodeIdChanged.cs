namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressPostCodeIdChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public Guid PostCodeId { get; init; }

    public AccessAddressPostCodeIdChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        Guid postCodeId)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        PostCodeId = postCodeId;
    }
}
