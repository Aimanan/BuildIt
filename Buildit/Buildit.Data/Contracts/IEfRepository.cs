using System.Linq;

namespace Buildit.Data.Contracts
{
    public interface IEfRepository<T>
        where T : class
    {
        IQueryable<T> All { get; }

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
