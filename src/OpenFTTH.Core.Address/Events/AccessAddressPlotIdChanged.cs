namespace OpenFTTH.Core.Address.Events;

public sealed record AccessAddressPlotIdChanged
{
    public Guid Id { get; init; }
    public DateTime? ExternalUpdatedDate { get; init; }
    public string? PlotId { get; init; }

    public AccessAddressPlotIdChanged(
        Guid id,
        DateTime? externalUpdatedDate,
        string? plotId)
    {
        Id = id;
        ExternalUpdatedDate = externalUpdatedDate;
        PlotId = plotId;
    }
}
