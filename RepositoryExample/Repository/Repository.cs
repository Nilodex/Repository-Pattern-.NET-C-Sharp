using Microsoft.EntityFrameworkCore;
using RepositoryExample.Interfaces;
using RepositoryExample.Models;

namespace RepositoryExample.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public RepositoryPatternContext _dbContext { get; set; }
        DbSet<T> _dbSet;
        public Repository(RepositoryPatternContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {

            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
