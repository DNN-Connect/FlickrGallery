using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class FlickrGalleryApiController : DnnApiController
    {
        private ContextHelper _flickrgalleryModuleContext;
        public ContextHelper FlickrGalleryModuleContext
        {
            get { return _flickrgalleryModuleContext ?? (_flickrgalleryModuleContext = new ContextHelper(this)); }
        }

        public HttpResponseMessage ServiceError(string message) {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
        }

        public HttpResponseMessage AccessViolation(string message)
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
        }

    }
}