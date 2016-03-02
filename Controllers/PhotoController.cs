using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{
    public class PhotoController : FlickrGalleryMvcController
    {
        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Add)]
        public ActionResult Upload()
        {
            return View();
        }

    }
}
