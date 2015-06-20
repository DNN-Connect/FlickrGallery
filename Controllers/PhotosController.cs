
using System.Collections.Generic;
using System.Linq;
using Connect.DNN.Modules.FlickrGallery.Models.Photos;
using Connect.DNN.Modules.FlickrGallery.Repositories;
using DotNetNuke.Collections;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{

    public partial class PhotosController
    {
        public static IPagedList<Photo> GetPage(int moduleId, int pageIndex, int pageSize)
        {
            return GetPage(moduleId, "DateTaken", pageIndex, pageSize);
        }

        public static IPagedList<Photo> GetPage(int moduleId, string orderByField, int pageIndex, int pageSize)
        {
            PhotoRepository repo = new PhotoRepository();
            IPagedList<Photo> res = repo.Find(pageIndex, pageSize, "WHERE ModuleId = @0 ORDER BY " + orderByField, moduleId);
            return res;
        }

        public static Dictionary<string, Photo> GetPhotos(int moduleId)
        {
            return GetPhotos(moduleId, "DateTaken");
        }

        public static Dictionary<string, Photo> GetPhotos(int moduleId, string orderByField)
        {
            PhotoRepository repo = new PhotoRepository();
            return repo.Find("WHERE ModuleId = @0 ORDER BY " + orderByField, moduleId).ToDictionary(p => p.FlickrId, p => p);
        }

        public static Photo GetPhoto(int photoId)
        {
            PhotoRepository repo = new PhotoRepository();
            return repo.GetById(photoId);
        }

        public static int AddPhoto(ref PhotoBase photo)
        {
            PhotoBaseRepository repo = new PhotoBaseRepository();
            repo.Insert(photo);
            return photo.PhotoId;
        }

        public static void UpdatePhoto(PhotoBase photo)
        {
            PhotoBaseRepository repo = new PhotoBaseRepository();
            repo.Update(photo);
        }

        public static void DeletePhoto(PhotoBase photo)
        {
            PhotoBaseRepository repo = new PhotoBaseRepository();
            repo.Delete(photo);
        }

    }
}
