namespace TsiryulnyaBot.DAL.Interface
{
    public interface IRepository<TEntity>
    {
        TEntity Get(Guid id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity id);
    }
}
