using System.Collections.Generic;
using IServices;
using ViewModels;
using Utility;
using System;
using System.Threading.Tasks;
using IRepositories;

namespace Services
{
    public class PhotoService : IPhotoService
    {
        private IPhotoRepository iPhotoRepository;

        public PhotoService(IPhotoRepository iPhotoRepository)
        {
            this.iPhotoRepository = iPhotoRepository;
        }

        public Task<List<Photo>> GetList(string filter, int start, int pageLimit)
        {
            List<Photo> list = new List<Photo>();
            List<DataModels.Photo> photoList = iPhotoRepository.GetList(filter, start, pageLimit).Result as List<DataModels.Photo>;
            if (photoList != null && photoList.Count > 0)
            {
                foreach (var p in photoList)
                {
                    Photo photo = Common.Mapper<Photo, DataModels.Photo>(p);
                    list.Add(photo);
                }
            }
            return Task.FromResult(list);
        }

        public Task<Photo> GetById(string id)
        {
            DataModels.Photo photo = iPhotoRepository.GetById(id).Result;
            return Task.FromResult(Common.Mapper<Photo, DataModels.Photo>(photo));
        }

        public Task<bool> Add(Photo photo)
        {
            DataModels.Photo p = Common.Mapper<DataModels.Photo, Photo>(photo);
            return iPhotoRepository.Add(p);
        }

        public Task<bool> Delete(string id)
        {
            return iPhotoRepository.Delete(id);
        }

        public Task<bool> Update(Photo photo)
        {
            DataModels.Photo p = Common.Mapper<DataModels.Photo, Photo>(photo);
            return iPhotoRepository.Update(p);
        }
    }
}
