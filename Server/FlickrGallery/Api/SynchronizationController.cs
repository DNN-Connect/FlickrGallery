using Connect.DNN.Modules.FlickrGallery.Common;
using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Connect.DNN.Modules.FlickrGallery.Api
{
    public class SynchronizationController : FlickrGalleryApiController
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Admin)]
        public HttpResponseMessage SyncModule()
        {
            switch (FlickrGalleryModuleContext.Settings.ViewType)
            {
                case ModuleSettings.ViewTypes.Album:
                    break;
                case ModuleSettings.ViewTypes.Group:
                    Synchronization.SyncGroup(FlickrGalleryModuleContext.Settings, ActiveModule.ModuleID);
                    break;
                case ModuleSettings.ViewTypes.None:
                    break;
                case ModuleSettings.ViewTypes.User:
                    Synchronization.SyncUser(FlickrGalleryModuleContext.Settings, ActiveModule.ModuleID);
                    break;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

    }
}