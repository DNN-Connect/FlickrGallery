
using System.Collections.Generic;
using System.Linq;
using Connect.DNN.Modules.FlickrGallery.Models.Photographers;
using Connect.DNN.Modules.FlickrGallery.Repositories;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{

    public partial class PhotographersController
    {

        public static Dictionary<string, Photographer> GetPhotographers(int moduleId)
        {
            PhotographerRepository repo = new PhotographerRepository();
            return repo.Get(moduleId).ToDictionary(p => p.FlickrId, p => p);
        }

        public static Photographer GetPhotographer(int photographerId)
        {
            PhotographerRepository repo = new PhotographerRepository();
            return repo.GetById(photographerId);
        }

        public static int AddPhotographer(ref PhotographerBase photographer)
        {
            PhotographerBaseRepository repo = new PhotographerBaseRepository();
            repo.Insert(photographer);
            return photographer.PhotographerId;
        }

        public static void UpdatePhotographer(PhotographerBase photographer)
        {
            PhotographerBaseRepository repo = new PhotographerBaseRepository();
            repo.Update(photographer);
        }

        public static void DeletePhotographer(PhotographerBase photographer)
        {
            PhotographerBaseRepository repo = new PhotographerBaseRepository();
            repo.Delete(photographer);
        }

    }
}
