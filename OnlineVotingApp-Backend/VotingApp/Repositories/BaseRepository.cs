using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TEntity GetByID(int id)
        {
            return dbSet.Find(id);           
        }

        public TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
             _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            TEntity entity=dbSet.Find(id);
              
            dbSet.Remove(entity);
            _context.SaveChanges();
            

        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}
