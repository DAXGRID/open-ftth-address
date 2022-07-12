using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class AddressProjection : ProjectionBase
{
    public HashSet<Guid> AccessAddressIds { get; } = new();
    public HashSet<Guid> RoadIds { get; } = new();
    public HashSet<Guid> PostCodeIds { get; } = new();

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
                RoadIds.Add(roadCreated.Id);
            });

        ProjectEvent<PostCodeCreated>(
            (@event) =>
            {
                var postCodeCreated = (PostCodeCreated)@event.Data;
                PostCodeIds.Add(postCodeCreated.Id);
            });
    }
}
