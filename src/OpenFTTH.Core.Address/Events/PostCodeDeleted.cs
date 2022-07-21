namespace OpenFTTH.Core.Address;

public sealed record PostCodeDeleted
{
    public Guid Id { get; init; }

    public PostCodeDeleted(Guid id)
    {
        Id = id;
    }
}
