using System.Linq.Expressions;

namespace TicketTix.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProp = null, bool track = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProp = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
