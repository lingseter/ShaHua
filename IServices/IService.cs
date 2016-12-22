using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Data List
        /// </summary>
        /// <param name="filter">eg:"Name like '%gester' and Gender =1"</param>
        /// <param name="start">page index</param>
        /// <param name="pageLimit">page count</param>
        /// <returns></returns>
        Task<List<TEntity>> GetList(string filter = null, int start = 0, int pageLimit = 10);

        Task<TEntity> GetById(string id);

        Task<bool> Add(TEntity photo);

        Task<bool> Delete(string id);

        Task<bool> Update(TEntity photo);
    }
}
