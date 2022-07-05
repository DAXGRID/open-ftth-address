using FluentResults;

namespace OpenFTTH.Address.Business;

public enum UnitAddressErrorCodes
{
    CANNOT_BE_EMPTY_GUID,
    CANNOT_BE_DEFAULT_DATE,
    UPDATED_CANNOT_BE_DEFAULT_DATE,
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
