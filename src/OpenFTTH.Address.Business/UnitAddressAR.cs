using FluentResults;
using OpenFTTH.Address.Business.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Address.Business;

public class UnitAddressAR : AggregateBase
{
    public Guid? OfficialAddressId { get; private set; }
    public Guid AccessAddressId { get; private set; }
    public Status Status { get; private set; }
    public string? FloorName { get; private set; }
    public string? SuitName { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }

    public UnitAddressAR()
    {
        // Register<UnitAddressCreated>(Apply);
    }

    public Result Create(
        Guid id,
        Guid? officialId,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime created,
        DateTime? updated)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new AddressError(AddressErrorCodes.UNIT_ADDRESS_ID_CANNOT_BE_NULL_OR_EMPTY,
                                 "Invalid work project id. Cannot be null or empty."));
        }

        if (id == Guid.Empty)
        {
            return Result.Fail(
                new AddressError(AddressErrorCodes.UNIT_ADDRESS_ID_CANNOT_BE_NULL_OR_EMPTY,
                                 "Invalid work project id. Cannot be null or empty."));
        }

        if (Created == new DateTime())
        {
            return Result.Fail(
                new AddressError(AddressErrorCodes.UNIT_ADDRESS_CREATED_CANNOT_BE_DEFAULT_DATE,
                                 $"{nameof(created)}, being default date, is invalid."));
        }

        Id = id;

        RaiseEvent(new UnitAddressCreated(
                       id: id,
                       officialId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       created: created,
                       updated: updated));

        return Result.Ok();
    }

    public Result Update(
        Guid? officialId,
        Guid accessAddressId,
        Status status,
        string? floorName,
        string? suitName,
        DateTime updated)
    {
        if (officialId == Guid.Empty)
        {
            return Result.Fail(
                new AddressError(AddressErrorCodes.UNIT_ADDRESS_ID_CANNOT_BE_NULL_OR_EMPTY,
                                 "Invalid work project id. Cannot be null or empty."));
        }

        if (updated == new DateTime())
        {
            return Result.Fail(
                new AddressError(AddressErrorCodes.UNIT_ADDRESS_CREATED_CANNOT_BE_DEFAULT_DATE,
                                 $"{nameof(updated)}, being default date, is invalid."));
        }

        RaiseEvent(new UnitAddressUpdated(
                       id: this.Id,
                       officialAddressId: officialId,
                       accessAddressId: accessAddressId,
                       status: status,
                       floorName: floorName,
                       suitName: suitName,
                       updated: updated));

        return Result.Ok();
    }
}
