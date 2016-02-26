using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Connect.DNN.Modules.FlickrGallery.Api
{
    public class PhotosController : FlickrGalleryApiController
    {

        [HttpGet()]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Page(int id, int albumId)
        {
            switch (FlickrGalleryModuleContext.Settings.ViewType)
            {
                case ModuleSettings.ViewTypes.Album:
                    break;
                case ModuleSettings.ViewTypes.Group:
                    return Request.CreateResponse(HttpStatusCode.OK, PhotoRepository.Instance.GetPage(ActiveModule.ModuleID, id, 50));
                case ModuleSettings.ViewTypes.None:
                    break;
                case ModuleSettings.ViewTypes.User:
                    if (albumId == -1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, AlbumPhotoRepository.Instance.GetPage(ActiveModule.ModuleID, albumId, id, 50));
                    }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

    }
}