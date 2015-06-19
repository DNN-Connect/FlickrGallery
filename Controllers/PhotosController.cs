
using System.Collections.Generic;
using System.Linq;
using Connect.DNN.Modules.FlickrGallery.Models.Photos;
using Connect.DNN.Modules.FlickrGallery.Repositories;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{

    public partial class PhotosController
    {

        public static Dictionary<string, Photo> GetPhotos(int moduleId)
        {
            PhotoRepository repo = new PhotoRepository();
            return repo.Get(moduleId).ToDictionary(p => p.FlickrId, p => p);
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
