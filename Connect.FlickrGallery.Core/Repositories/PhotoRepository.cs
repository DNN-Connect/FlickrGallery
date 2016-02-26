using System;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.FlickrGallery.Core.Models.Photos;
using DotNetNuke.Collections;
using System.Linq;

namespace Connect.FlickrGallery.Core.Repositories
{
    public class PhotoRepository : ServiceLocator<IPhotoRepository, PhotoRepository>, IPhotoRepository
    {
        protected override Func<IPhotoRepository> GetFactory()
        {
            return () => new PhotoRepository();
        }
        public IPagedList<Photo> GetPage(int moduleId, int pageIndex, int pageSize)
        {
            return GetPage(moduleId, "DateTaken", pageIndex, pageSize);
        }
        public IPagedList<Photo> GetPage(int moduleId, string orderByField, int pageIndex, int pageSize)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Photo>();
                IPagedList<Photo> res = rep.Find(pageIndex, pageSize, "WHERE ModuleId = @0 ORDER BY " + orderByField, moduleId);
                return res;
            }
        }
        public Dictionary<string, Photo> GetPhotos(int moduleId)
        {
            return GetPhotos(moduleId, "DateTaken");
        }
        public Dictionary<string, Photo> GetPhotos(int moduleId, string orderByField)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Photo>();
                return rep.Find("WHERE ModuleId = @0 ORDER BY " + orderByField, moduleId).ToDictionary(p => p.FlickrId, p => p);
            }
        }
        public IEnumerable<Photo> GetPhotosByPhotographer(int photographerId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Photo>(System.Data.CommandType.Text,
                    "SELECT * FROM vw_Connect_FlickrGallery_Photos WHERE PhotographerId=@0",
                    photographerId);
            }
        }
        public Photo GetPhoto(int moduleId, int photoId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Photo>();
                return rep.GetById(photoId, moduleId);
            }
        }
        public int AddPhoto(ref PhotoBase photo)
        {
            Requires.NotNull(photo);
            Requires.PropertyNotNegative(photo, "ModuleId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotoBase>();
                rep.Insert(photo);
            }
            return photo.PhotoId;
        }
        public void DeletePhoto(PhotoBase photo)
        {
            Requires.NotNull(photo);
            Requires.PropertyNotNegative(photo, "PhotoId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotoBase>();
                rep.Delete(photo);
            }
        }
        public void DeletePhoto(int moduleId, int photoId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotoBase>();
                rep.Delete("WHERE ModuleId = @0 AND PhotoId = @1", moduleId, photoId);
            }
        }
        public void UpdatePhoto(PhotoBase photo)
        {
            Requires.NotNull(photo);
            Requires.PropertyNotNegative(photo, "PhotoId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotoBase>();
                rep.Update(photo);
            }
        }
    }

    public interface IPhotoRepository
    {
        IPagedList<Photo> GetPage(int moduleId, int pageIndex, int pageSize);
        IPagedList<Photo> GetPage(int moduleId, string orderByField, int pageIndex, int pageSize);
        Dictionary<string, Photo> GetPhotos(int moduleId);
        Dictionary<string, Photo> GetPhotos(int moduleId, string orderByField);
        IEnumerable<Photo> GetPhotosByPhotographer(int photographerId);
        Photo GetPhoto(int moduleId, int photoId);
        int AddPhoto(ref PhotoBase photo);
        void DeletePhoto(PhotoBase photo);
        void DeletePhoto(int moduleId, int photoId);
        void UpdatePhoto(PhotoBase photo);
    }
}
