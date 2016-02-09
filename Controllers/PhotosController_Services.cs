using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Repositories;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;
using Connect.FlickrGallery.Core.Models.AlbumPhotos;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{

    public partial class PhotosController : FlickrGalleryApiController
    {

        #region " Service Methods "
        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Page(int id, string view)
        {
            var ctrl = "~/DesktopModules/Connect/FlickrGallery/Views/ServiceViews/GallerySegment.cshtml";
            if (view == "album")
            {
                 ctrl = "~/DesktopModules/Connect/FlickrGallery/Views/ServiceViews/AlbumSegment.cshtml";
            }
            RazorControl ctl = new RazorControl(ActiveModule,
                ctrl,
                Globals.SharedResourceFileName);
            var nextPage = new ContentPage(id);
            StringContent content = new StringContent(ctl.RenderObject(nextPage), Encoding.UTF8, "text/html");
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            return res;
        }

        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage List()
        {
            List<PhotoSwipePhoto> res = new List<PhotoSwipePhoto>();
            foreach (Connect.FlickrGallery.Core.Models.Photos.Photo p in PhotoRepository.Instance.GetPhotos(ActiveModule.ModuleID).Values.OrderBy(p => p.DateTaken))
            {
                res.Add(new PhotoSwipePhoto() { src = p.LargeUrl, w = p.LargeWidth, h = p.LargeHeight, title = p.Title });
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Album(int album)
        {
            List<PhotoSwipePhoto> res = new List<PhotoSwipePhoto>();
            foreach (AlbumPhoto p in AlbumPhotoRepository.Instance.GetAlbumPhotosByAlbum(album).OrderBy(p => p.DateTaken))
            {
                res.Add(new PhotoSwipePhoto() { src = p.LargeUrl, w = p.LargeWidth, h = p.LargeHeight, title = p.Title });
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public class PhotoSwipePhoto
        {
            public string src { get; set; }
            public int? w { get; set; }
            public int? h { get; set; }
            public string title { get; set; }
        }
        #endregion

    }
}

