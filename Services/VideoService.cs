using System;
using System.Collections.Generic;
using IServices;
using ViewModels;
using Utility;
using System.Threading.Tasks;
using IRepositories;

namespace Services
{
    public class VideoService : IVideoService
    {
        private IVideoRepository iVideoRepository;

        public VideoService(IVideoRepository iVideoRepository)
        {
            this.iVideoRepository = iVideoRepository;
        }

        public Task<bool> Add(Video video)
        {
            DataModels.Video v = Common.Mapper<DataModels.Video, Video>(video);
            return iVideoRepository.Add(v);
        }

        public Task<bool> Delete(string id)
        {
            return iVideoRepository.Delete(id);
        }

        public Task<List<Video>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            List<Video> list = new List<Video>();
            List<DataModels.Video> videoList = iVideoRepository.GetList(filter, start, pageLimit).Result as List<DataModels.Video>;
            if (videoList != null && videoList.Count > 0)
            {
                foreach (var v in videoList)
                {
                    Video video = Common.Mapper<Video, DataModels.Video>(v);
                    list.Add(video);
                }
            }
            return Task.FromResult(list);
        }

        public Task<Video> GetById(string id)
        {
            DataModels.Video video = iVideoRepository.GetById(id).Result;
            return Task.FromResult(Common.Mapper<Video, DataModels.Video>(video));
        }

        public Task<bool> Update(Video video)
        {
            DataModels.Video v = Common.Mapper<DataModels.Video, Video>(video);
            return iVideoRepository.Update(v);
        }
    }
}
