using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Helpers;
using VotingApp.Interfaces;

namespace VotingApp
{
    public class BaseRepository <TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MasterContext _context;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(MasterContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByID<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);           
        }

        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);

            _context.SaveChanges();
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }
}
