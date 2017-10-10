namespace Buildit.Common.Providers.Contracts
{
    public interface IUserProvider
    {
        string GetUserId();

        string GetUsername();
    }
}