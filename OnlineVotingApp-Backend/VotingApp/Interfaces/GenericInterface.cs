using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);

        Task<TEntity> GetByID<TKey>(TKey id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
    }
}
