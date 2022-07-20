using FluentResults;
using OpenFTTH.Core.Address.Events;
using OpenFTTH.EventSourcing;

namespace OpenFTTH.Core.Address;

public class RoadAR : AggregateBase
{
    public string? OfficialId { get; private set; }
    public string? Name { get; private set; }

    public RoadAR()
    {
        Register<RoadCreated>(Apply);
        Register<RoadUpdated>(Apply);
    }

    public Result Create(Guid id, string officialId, string name)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                    $"{nameof(id)} cannot be empty guid."));
        }

        if (String.IsNullOrWhiteSpace(officialId))
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.OFFICIAL_ID_CANNOT_BE_WHITE_SPACE_OR_NULL,
                    $"{nameof(officialId)} is not allowed to be whitespace or null."));
        }

        Id = id;

        RaiseEvent(new RoadCreated(
            id: id,
            officialId: officialId,
            name: name));

        return Result.Ok();
    }

    public Result Update(string name)
    {
        if (Id == Guid.Empty)
        {
            return Result.Fail(
                new RoadError(
                    RoadErrorCode.ID_CANNOT_BE_EMPTY_GUID,
                    @$"{nameof(Id)}, being default guid is not valid,
 the AR has most likely not being created yet."));
        }

        RaiseEvent(new RoadUpdated(Id, name));
        return Result.Ok();
    }

    private void Apply(RoadCreated roadCreated)
    {
        OfficialId = roadCreated.OfficialId;
        Name = roadCreated.Name;
    }

    private void Apply(RoadUpdated roadUpdated)
    {
        Name = roadUpdated.Name;
    }
}
