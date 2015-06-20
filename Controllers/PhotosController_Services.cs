
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Connect.DNN.Modules.FlickrGallery.Common;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{

    public partial class PhotosController : FlickrGalleryApiController
    {

        #region " Service Methods "
        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Page(int id)
        {
            RazorControl ctl = new RazorControl(ActiveModule,
                "~/DesktopModules/Connect/FlickrGallery/Views/ServiceViews/GallerySegment.cshtml",
                Globals.SharedResourceFileName);
            StringContent content = new StringContent(ctl.RenderObject(id), Encoding.UTF8, "text/html");
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            return res;
        }

        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage List()
        {
            List<PhotoSwipePhoto> res = new List<PhotoSwipePhoto>();
            foreach (Models.Photos.Photo p in GetPhotos(ActiveModule.ModuleID).Values)
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

