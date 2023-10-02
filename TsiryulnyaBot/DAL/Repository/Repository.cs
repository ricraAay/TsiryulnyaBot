using Microsoft.EntityFrameworkCore;
using TsiryulnyaBot.DAL.Interface;

namespace TsiryulnyaBot.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TsiryulnyaContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public Repository(TsiryulnyaContext context) 
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
