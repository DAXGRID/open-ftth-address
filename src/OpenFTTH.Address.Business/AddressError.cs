using FluentResults;

namespace OpenFTTH.Address.Business;

public class AddressError : Error
{
    public AddressErrorCodes Code { get; init; }

    public AddressError(AddressErrorCodes errorCode, string errorMsg)
        : base(errorCode.ToString() + ": " + errorMsg)
    {
        Code = errorCode;
        Metadata.Add("ErrorCode", errorCode.ToString());
    }
}
