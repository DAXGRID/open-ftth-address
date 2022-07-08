using OpenFTTH.Address.Business.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class AddressProjection : ProjectionBase
{
    private readonly HashSet<Guid> _accessAddressIds = new();
    public HashSet<Guid> AccessAddressIds => _accessAddressIds;

    public AddressProjection()
    {
        ProjectEvent<AccessAddressCreated>(
            (@event) =>
            {
                var accessAddressCreated = (AccessAddressCreated)@event.Data;
                AccessAddressIds.Add(accessAddressCreated.Id);
            });
    }
}
