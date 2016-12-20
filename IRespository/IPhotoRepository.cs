using System;
using System.Collections.Generic;
using DataModels;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IPhotoRepository
    {
        Task<List<Photo>> GetList(string filter = null, int start = 0, int pageLimit = 10);

        Task<Photo> GetPhotoById(Guid id);

        Task<bool> Add(Photo photo);

        Task<bool> Delete(string id);

        Task<bool> Update(Photo photo);
    }
}
