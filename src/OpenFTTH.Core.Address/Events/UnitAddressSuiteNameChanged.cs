namespace OpenFTTH.Core.Address.Events;

public sealed record UnitAddressSuiteNameChanged
{
    public Guid Id { get; init; }
    public string? SuiteName { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }

    public UnitAddressSuiteNameChanged(
        Guid id,
        string? suiteName,
        DateTime? externalUpdatedDate)
    {
        Id = id;
        SuiteName = suiteName;
        ExternalUpdatedDate = externalUpdatedDate;
    }
}
