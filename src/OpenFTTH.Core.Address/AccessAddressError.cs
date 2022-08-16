using FluentResults;

namespace OpenFTTH.Core.Address;

public enum AccessAddressErrorCodes
{
    ID_CANNOT_BE_EMPTY_GUID,
    ROAD_DOES_NOT_EXIST,
    POST_CODE_DOES_NOT_EXIST,
    CANNOT_UPDATE_DELETED,
    CANNOT_DELETE_ALREADY_DELETED,
    NO_CHANGES
}

public class AccessAddressError : Error
{
    public AccessAddressErrorCodes Code { get; init; }

    public AccessAddressError(AccessAddressErrorCodes errorCode, string errorMsg)
        : base(errorCode.ToString() + ": " + errorMsg)
    {
        Code = errorCode;
        Metadata.Add("ErrorCode", errorCode.ToString());
    }
}
