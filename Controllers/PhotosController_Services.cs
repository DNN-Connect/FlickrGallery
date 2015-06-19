
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
		public HttpResponseMessage Page(int id, string view)
		{
            RazorControl ctl = new RazorControl(ActiveModule,
                "~/DesktopModules/Connect/FlickrGallery/Views/ServiceViews/GallerySegment.cshtml",
                Globals.SharedResourceFileName);
            StringContent content = new StringContent(ctl.RenderObject(id), Encoding.UTF8, "text/html");
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            return res;
        }
		#endregion

	}
}

