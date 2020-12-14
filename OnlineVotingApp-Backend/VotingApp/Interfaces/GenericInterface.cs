using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(int id);

        TEntity GetByID(int id);

        IEnumerable<TEntity> GetAll();

        TEntity Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
    }
}
