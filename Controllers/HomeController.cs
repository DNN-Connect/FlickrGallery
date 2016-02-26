using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{
    public class HomeController : FlickrGalleryMvcController
    {
        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Index()
        {
            switch (FlickrGalleryModuleContext.Settings.ViewType)
            {
                case ModuleSettings.ViewTypes.Album:
                    return View();
                case ModuleSettings.ViewTypes.Group:
                    return View("Group");
                case ModuleSettings.ViewTypes.None:
                    return View();
                case ModuleSettings.ViewTypes.User:
                    return View("User");
            }
            return View();
        }
    }
}
