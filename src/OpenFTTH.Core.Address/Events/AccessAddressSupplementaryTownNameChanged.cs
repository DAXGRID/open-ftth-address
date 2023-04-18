namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressSupplementaryTownNameChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string? SupplementaryTownName { get; init; }

    public AccessAddressSupplementaryTownNameChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string? supplementaryTownName)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        SupplementaryTownName = supplementaryTownName;
    }
}
