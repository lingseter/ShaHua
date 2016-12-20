using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using IRepository;

namespace Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        public Task<bool> Add(Photo photo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Photo>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Photo> GetPhotoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
