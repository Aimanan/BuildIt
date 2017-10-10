using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Buildit.Data
{
    public class BuilditDbContext : IdentityDbContext<User>, IBuilditDbContext
    {
        public BuilditDbContext()
            : base("BuilditConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Publication> Publications { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<PublicationType> PublicationTypes { get; set; }

        public static BuilditDbContext Create()
        {
            return new BuilditDbContext();
        }

        IDbSet<T> IBuilditDbContext.Set<T>()
        {
            return this.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
