using FluentResults;

namespace OpenFTTH.Address.Business;

public enum UnitAddressErrorCodes
{
    ID_CANNOT_BE_EMPTY_GUID,
    ACCESS_ADDRESS_ID_CANNOT_BE_EMPTY_GUID,
    CREATED_CANNOT_BE_DEFAULT_DATE,
    UPDATED_CANNOT_BE_DEFAULT_DATE,
    ID_NOT_SET,
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
