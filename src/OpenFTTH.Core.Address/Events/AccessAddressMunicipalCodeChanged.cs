namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressMunicipalCodeChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string MunicipalCode { get; init; }

    public AccessAddressMunicipalCodeChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string municipalCode)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        MunicipalCode = municipalCode;
    }
}
