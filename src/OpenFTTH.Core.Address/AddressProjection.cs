using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class AddressProjection : ProjectionBase
{
    public HashSet<Guid> AccessAddressIds { get; } = new();
    public Dictionary<string, Guid> AccessAddressOfficialIdToId { get; } = new();
    public Dictionary<string, Guid> UnitAddressOfficialIdToId { get; } = new();
    public Dictionary<string, Guid> RoadOfficialIdIdToId { get; } = new();
    public Dictionary<string, Guid> PostCodeNumberToId { get; } = new();

    public HashSet<Guid> GetRoadIds() => RoadOfficialIdIdToId.Values.ToHashSet();
    public HashSet<Guid> GetPostCodeIds() => PostCodeNumberToId.Values.ToHashSet();

    public AddressProjection()
    {
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
            default:
                throw new ArgumentException($"Could not handle typeof '{eventEnvelope.Data.GetType().Name}'");
        }

        return Task.CompletedTask;
    }

    private void Handle(AccessAddressCreated accessAddressCreated)
    {
        if (accessAddressCreated.OfficialId is not null)
        {
            AccessAddressOfficialIdToId.Add(
                accessAddressCreated.OfficialId, accessAddressCreated.Id);
        }

        // This is a bit special since we allow access addresses to be created
        // without 'officialIds' so we cannot use the values from the
        // official id to project an internal id lookup table,
        // so we have to keep a seperate lookup table in sync.
        // This is needed so we can check if access address exists.
        AccessAddressIds.Add(accessAddressCreated.Id);
    }

    private void Handle(UnitAddressCreated unitAddressCreated)
    {
        if (unitAddressCreated.OfficialId is not null)
        {
            UnitAddressOfficialIdToId.Add(
                unitAddressCreated.OfficialId, unitAddressCreated.Id);
        }
    }

    private void Handle(RoadCreated roadCreated)
    {
        RoadOfficialIdIdToId.Add(roadCreated.OfficialId, roadCreated.Id);
    }

    private void Handle(PostCodeCreated postCodeCreated)
    {
        PostCodeNumberToId.Add(postCodeCreated.Number, postCodeCreated.Id);
    }
}
