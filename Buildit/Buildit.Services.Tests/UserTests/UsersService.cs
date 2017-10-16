using Buildit.Data.Contracts;

namespace Buildit.Services.Tests.UserTests
{
    internal class UsersService
    {
        private IBuilditData @object;

        public UsersService(IBuilditData @object)
        {
            this.@object = @object;
        }
    }
}