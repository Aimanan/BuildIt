using Buildit.Data.Contracts;
using Bytes2you.Validation;
using System.Linq;
using System;
using Buildit.Services.Contracts;
using Buildit.Data.Models;

namespace Buildit.Services
{
    public class UsersService : IUsersService
    {
        private readonly IBuilditData data;
        private readonly IEfRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IBuilditData data,
            IEfRepository<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(data, "Data").IsNull().Throw();
            Guard.WhenArgument(userRepository, "UserRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "UnitOfWork").IsNull().Throw();

            this.data = data;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool CheckIfUserExists(string username)
        {
            var exists = this.data.Users.All.Any(x => x.UserName == username);
            return exists;
        }

        public User GetById(string id)
        {
            var user = this.userRepository
                .All
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return user;
        }

        public void BanUser(string userId)
        {
            var user = this.userRepository
                .All
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            user.IsBanned = true;
            this.userRepository.Update(user);
            this.unitOfWork.Commit();

        }

        public void UnbanUser(string userId)
        {
            var user = this.userRepository
                .All
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            user.IsBanned = false;
            this.userRepository.Update(user);
            this.unitOfWork.Commit();
        }
    }
}
