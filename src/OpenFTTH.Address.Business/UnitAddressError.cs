using FluentResults;

namespace OpenFTTH.Address.Business;

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
