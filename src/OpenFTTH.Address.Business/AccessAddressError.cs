using FluentResults;

namespace OpenFTTH.Address.Business;

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
