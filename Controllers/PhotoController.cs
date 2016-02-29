using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Repositories;
using System.Web;
using System.IO;
using System;

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

        public class SubmitPhotos
        {
            public string UploadedFiles { get; set; }
            public int SelectedAlbum { get; set; }
            public string NewAlbum { get; set; }
        }

        [HttpPost]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Add)]
        public ActionResult Upload(SubmitPhotos data)
        {
            return RedirectToDefaultRoute();
        }
    }
}
