using FluentResults;

namespace OpenFTTH.Address.Business;

public enum AccessAddressErrorCodes
{
    ID_CANNOT_BE_EMPTY_GUID,
    CREATED_CANNOT_BE_DEFAULT_DATE,
    UPDATED_CANNOT_BE_DEFAULT_DATE,
    ROAD_DOES_NOT_EXIST,
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
