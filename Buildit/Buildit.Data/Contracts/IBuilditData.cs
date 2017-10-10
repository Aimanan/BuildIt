using Buildit.Data.Models;

namespace Buildit.Data.Contracts
{
    public interface IBuilditData
    {
        IEfRepository<Publication> Publications { get; }

        IEfRepository<PublicationType> PublicationTypes { get; }

        IEfRepository<Rating> Ratings { get; }

        IEfRepository<User> Users { get; }

        void SaveChanges();
    }
}