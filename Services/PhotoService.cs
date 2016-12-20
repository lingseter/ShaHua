﻿using System.Collections.Generic;
using IServices;
using ViewModels;
using Utility;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class PhotoService : IPhotoService
    {
        private IRepository.IPhotoRepository iPhotoRepository;

        public PhotoService(IRepository.IPhotoRepository iPhotoRepository)
        {
            this.iPhotoRepository = iPhotoRepository;
        }

        public Task<List<Photo>> GetList(string filter, int start, int pageLimit)
        {
            List<Photo> list = new List<Photo>();
            List<DataModels.Photo> photoList = iPhotoRepository.GetList(filter, start, pageLimit).Result;
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

        public Task<Photo> GetPhotoById(Guid id)
        {
            DataModels.Photo photo = iPhotoRepository.GetPhotoById(id).Result;
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
