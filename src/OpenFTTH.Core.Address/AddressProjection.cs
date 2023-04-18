using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class AddressProjection : ProjectionBase
{
    public HashSet<Guid> AccessAddressIds { get; } = new();
    public Dictionary<string, Guid> AccessAddressExternalIdToId { get; } = new();
    public Dictionary<string, Guid> UnitAddressExternalIdToId { get; } = new();
    public Dictionary<string, Guid> RoadExternalIdIdToId { get; } = new();
    public Dictionary<string, Guid> PostCodeNumberToId { get; } = new();

    public HashSet<Guid> GetRoadIds() => RoadExternalIdIdToId.Values.ToHashSet();
    public HashSet<Guid> GetPostCodeIds() => PostCodeNumberToId.Values.ToHashSet();

    public AddressProjection()
    {
        // Only interested in `Created` events, because the projection only cares about values that never changes.
        ProjectEventAsync<AccessAddressCreated>(ProjectAsync);
        ProjectEventAsync<UnitAddressCreated>(ProjectAsync);
        ProjectEventAsync<RoadCreated>(ProjectAsync);
        ProjectEventAsync<PostCodeCreated>(ProjectAsync);
    }

    private Task ProjectAsync(IEventEnvelope eventEnvelope)
    {
        switch (eventEnvelope.Data)
        {
            case (AccessAddressCreated @event):
                Handle(@event);
                break;
            case (UnitAddressCreated @event):
                Handle(@event);
                break;
            case (RoadCreated @event):
                Handle(@event);
                break;
            case (PostCodeCreated @event):
                Handle(@event);
                break;
            default:
                throw new ArgumentException(
                    $"Could not handle typeof '{eventEnvelope.Data.GetType().Name}'");
        }

        return Task.CompletedTask;
    }

    private void Handle(AccessAddressCreated accessAddressCreated)
    {
        if (accessAddressCreated.ExternalId is not null)
        {
            AccessAddressExternalIdToId.Add(
                accessAddressCreated.ExternalId, accessAddressCreated.Id);
        }

        // This is a bit special since we allow access addresses to be created
        // without 'externalIds' so we cannot use the values from the
        // official id to project an internal id lookup table,
        // so we have to keep a seperate lookup table in sync.
        // This is needed so we can check if access address exists.
        AccessAddressIds.Add(accessAddressCreated.Id);
    }

    private void Handle(UnitAddressCreated unitAddressCreated)
    {
        if (unitAddressCreated.ExternalId is not null)
        {
            UnitAddressExternalIdToId.Add(
                unitAddressCreated.ExternalId, unitAddressCreated.Id);
        }
    }

    private void Handle(RoadCreated roadCreated)
    {
        RoadExternalIdIdToId.Add(roadCreated.ExternalId, roadCreated.Id);
    }

    private void Handle(PostCodeCreated postCodeCreated)
    {
        PostCodeNumberToId.Add(postCodeCreated.Number, postCodeCreated.Id);
    }
}
