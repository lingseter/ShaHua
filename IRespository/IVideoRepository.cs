using System;
using System.Collections.Generic;
using DataModels;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetList(string filter = null, int start = 0, int pageLimit = 10);

        Task<Video> GetVideoById(Guid id);

        Task<bool> Add(Video photo);

        Task<bool> Delete(string id);

        Task<bool> Update(Video photo);
    }
}
