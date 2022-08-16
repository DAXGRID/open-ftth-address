using FluentResults;

namespace OpenFTTH.Core.Address;

public enum UnitAddressErrorCodes
{
    ID_CANNOT_BE_EMPTY_GUID,
    ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID,
    EXTERNAL_CREATED_DATE_CANNOT_BE_DEFAULT_DATE,
    EXTERNAL_UPDATED_DATE_CANNOT_BE_DEFAULT_DATE,
    ID_NOT_SET,
    ACCESS_ADDRESS_DOES_NOT_EXISTS,
    CANNOT_UPDATE_DELETED,
    CANNOT_DELETE_ALREADY_DELETED,
    NO_CHANGES
}

public class UnitAddressError : Error
{
    public UnitAddressErrorCodes Code { get; init; }

    public UnitAddressError(UnitAddressErrorCodes errorCode, string errorMsg)
        : base(errorCode.ToString() + ": " + errorMsg)
    {
        Code = errorCode;
        Metadata.Add("ErrorCode", errorCode.ToString());
    }
}
