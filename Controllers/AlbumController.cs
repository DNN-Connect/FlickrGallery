using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Repositories;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{
    public class AlbumController: FlickrGalleryMvcController
    {
        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Album(int albumId)
        {
            DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
            var album = AlbumRepository.Instance.GetAlbum(ActiveModule.ModuleID, albumId);
            return View(album);
        }
    }
}
