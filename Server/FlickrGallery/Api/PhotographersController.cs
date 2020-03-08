
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Connect.DNN.Modules.FlickrGallery.Common;

namespace Connect.DNN.Modules.FlickrGallery.Api
{

	public partial class PhotographersController : FlickrGalleryApiController
	{

		#region " Service Methods "
		[HttpGet()]
		[DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
		public HttpResponseMessage MyMethod(int id)
		{
			bool res = true;
			return Request.CreateResponse(HttpStatusCode.OK, res);
		}
		#endregion

	}
}

