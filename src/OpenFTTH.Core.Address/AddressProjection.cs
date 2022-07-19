using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class AddressProjection : ProjectionBase
{
    public HashSet<Guid> AccessAddressIds { get; } = new();

    public Dictionary<string, Guid> RoadExternalIdToId { get; } = new();
    public HashSet<Guid> RoadIds => RoadExternalIdToId.Values.ToHashSet();

    public Dictionary<string, Guid> PostCodeNumberToId { get; } = new();
    public HashSet<Guid> PostCodeIds => PostCodeNumberToId.Values.ToHashSet();

    public AddressProjection()
    {
        ProjectEvent<AccessAddressCreated>(
            (@event) =>
            {
                var accessAddressCreated = (AccessAddressCreated)@event.Data;
                AccessAddressIds.Add(accessAddressCreated.Id);
            });

        ProjectEvent<RoadCreated>(
            (@event) =>
            {
                var roadCreated = (RoadCreated)@event.Data;
                RoadExternalIdToId.Add(roadCreated.ExternalId, roadCreated.Id);
            });

        ProjectEvent<PostCodeCreated>(
            (@event) =>
            {
                var postCodeCreated = (PostCodeCreated)@event.Data;
                PostCodeNumberToId.Add(postCodeCreated.Number, postCodeCreated.Id);
            });
    }
}
