using DataModels;
using IRepositories;

namespace Repositories
{
    public class VideoRepository : DataRepository<Video>, IVideoRepository
    {
        public VideoRepository()
        {
            TableName = "Video";
        }
    }
}
