using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace IServices
{
    public interface IVideoService
    {
        Task<List<Video>> GetList(string filter = null, int start = 0, int pageLimit = 10);

        Task<Video> GetVideoById(Guid id);

        Task<bool> Add(Video video);

        Task<bool> Delete(string id);

        Task<bool> Update(Video video);
    }
}
