using DataModels;
using IRepositories;

namespace Repositories
{
    public class PhotoRepository : DataRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository()
        {
            TableName = "Photo";
        }
    }
}
