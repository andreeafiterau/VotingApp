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

        //verific inainte
        public TEntity GetByID(int id)
        {
            //rezultatul poate sa fie null, argumentul poate sa fie null
            return dbSet.Find(id);           
        }

        public TEntity Insert(TEntity entity)
        {
            //entitatea poate sa fie nula sau poate sa fie formatata gresit
            dbSet.Add(entity);
             _context.SaveChanges();//sys errors
            //string =! entity
            return entity;
        }

        public void Delete(int id)
        {
            TEntity entity=dbSet.Find(id);//argument null, rez null
              
            dbSet.Remove(entity);
            _context.SaveChanges();//idem mai sus
            

        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            _context.SaveChanges();//idem sus

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();//lista nula
        }
    }
}
