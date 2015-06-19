
using System.Net;
using System.Net.Http;
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
		public HttpResponseMessage MyMethod(int id)
		{
			bool res = true;
			return Request.CreateResponse(HttpStatusCode.OK, res);
		}
		#endregion

	}
}

