using System.Linq.Expressions;

namespace TsiryulnyaBot.DAL.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Aggregate(params Expression<Func<TEntity, bool>>[] includeProperties);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity id);

        int Commit();
    }
}
