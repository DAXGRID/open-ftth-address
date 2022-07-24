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
        ProjectEvent<AccessAddressCreated>(
            (@event) =>
            {
                var accessAddressCreated = (AccessAddressCreated)@event.Data;
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
            });

        ProjectEvent<UnitAddressCreated>(
            (@event) =>
            {
                var unitAddressCreated = (UnitAddressCreated)@event.Data;
                if (unitAddressCreated.OfficialId is not null)
                {
                    UnitAddressOfficialIdToId.Add(
                        unitAddressCreated.OfficialId, unitAddressCreated.Id);
                }
            });

        ProjectEvent<RoadCreated>(
            (@event) =>
            {
                var roadCreated = (RoadCreated)@event.Data;
                RoadOfficialIdIdToId.Add(roadCreated.OfficialId, roadCreated.Id);
            });

        ProjectEvent<PostCodeCreated>(
            (@event) =>
            {
                var postCodeCreated = (PostCodeCreated)@event.Data;
                PostCodeNumberToId.Add(postCodeCreated.Number, postCodeCreated.Id);
            });
    }
}
