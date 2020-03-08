using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Connect.DNN.Modules.FlickrGallery.Api
{
    public class AlbumsController : FlickrGalleryApiController
    {

        [HttpGet()]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage List()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AlbumRepository.Instance.GetAlbums(ActiveModule.ModuleID));
        }

    }
}