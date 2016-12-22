using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(string id);
        /// <summary>
        /// Get Data List
        /// </summary>
        /// <param name="filter">eg:"Name like '%gester' and Gender =1"</param>
        /// <param name="start">page index</param>
        /// <param name="pageLimit">page count</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetList(string filter = null, int start = 0, int pageLimit = 10);
        Task<bool> Add(TEntity entity);
        Task<bool> AddRange(IEnumerable<TEntity> entities);
        Task<bool> Delete(string id);
        Task<bool> DeleteRange(IEnumerable<string> idList);
        Task<bool> Update(TEntity entity);

    }
}
