using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Bytes2you.Validation;
using System;


namespace Buildit.Data
{
    public class BuilditData : IBuilditData
    {
        private readonly IBuilditDbContext dbContext;

        public BuilditData(
            IBuilditDbContext dbContext,
            IEfRepository<User> usersRepo,
            IEfRepository<Publication> publicationRepo,
            IEfRepository<PublicationType> TypesRepo,
            IEfRepository<Rating> ratingsRepo)
        {
            Guard.WhenArgument(dbContext, "DbContext").IsNull().Throw();
            Guard.WhenArgument(usersRepo, "UsersRepo").IsNull().Throw();
            Guard.WhenArgument(publicationRepo, "PublicationRepo").IsNull().Throw();
            Guard.WhenArgument(TypesRepo, "TypesRepo").IsNull().Throw();
            Guard.WhenArgument(ratingsRepo, "ratingsRepo").IsNull().Throw();

            this.dbContext = dbContext;
            this.Users = usersRepo;
            this.Publications = publicationRepo;
            this.PublicationTypes = TypesRepo;
            this.Ratings = ratingsRepo;
        }

        public IEfRepository<User> Users { get; private set; }

        public IEfRepository<Publication> Publications { get; private set; }

        public IEfRepository<PublicationType> PublicationTypes { get; private set; }

        public IEfRepository<Rating> Ratings { get; private set; }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
