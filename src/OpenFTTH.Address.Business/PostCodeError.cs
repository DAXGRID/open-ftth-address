using FluentResults;

namespace OpenFTTH.Address.Business;

public enum PostCodeErrorCodes
{
    ID_CANNOT_BE_EMPTY_GUID,
    CANNOT_BE_EMPTY_NULL_OR_WHITESPACE,
}

public class PostCodeError : Error
{
    public PostCodeErrorCodes Code { get; init; }

    public PostCodeError(PostCodeErrorCodes errorCode, string errorMsg)
        : base(errorCode.ToString() + ": " + errorMsg)
    {
        Code = errorCode;
        Metadata.Add("ErrorCode", errorCode.ToString());
    }
}
