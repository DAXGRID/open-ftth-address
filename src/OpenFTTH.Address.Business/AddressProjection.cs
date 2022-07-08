using OpenFTTH.Address.Business.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class AddressProjection : ProjectionBase
{
    public HashSet<Guid> AccessAddressIds { get; } = new();
    public HashSet<Guid> RoadIds { get; } = new();

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
    }
}
