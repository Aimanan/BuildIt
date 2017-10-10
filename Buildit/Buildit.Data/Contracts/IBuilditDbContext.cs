using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buildit.Data.Models;
using System.Data.Entity.Infrastructure;

namespace Buildit.Data.Contracts
{
    public interface IBuilditDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Publication> Publications { get; set; }

        IDbSet<PublicationType> PublicationTypes { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
