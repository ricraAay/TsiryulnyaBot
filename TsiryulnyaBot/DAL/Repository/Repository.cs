using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<TEntity> Aggregate(params Expression<Func<TEntity, bool>>[] includeProperties)
        {
            return includeProperties
                .Aggregate(_dbSet.AsNoTracking(), (current, includeProperty) => current.Where(includeProperty))
                .ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
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

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
