using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using IRepository;

namespace Repository
{
    public class VideoRepository : IVideoRepository
    {
        public Task<bool> Add(Video photo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Video> GetVideoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Video photo)
        {
            throw new NotImplementedException();
        }
    }
}
