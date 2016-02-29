using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;
using FlickrNet;
using System.Web.Routing;
using DotNetNuke.Web.Mvc.Routing;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{
    public class HomeController : FlickrGalleryMvcController
    {
        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Index()
        {
            switch (FlickrGalleryModuleContext.Settings.ViewType)
            {
                case ModuleSettings.ViewTypes.Album:
                    return View();
                case ModuleSettings.ViewTypes.Group:
                    return View("Group");
                case ModuleSettings.ViewTypes.None:
                    return View();
                case ModuleSettings.ViewTypes.User:
                    return View("User");
            }
            return View();
        }

        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Admin)]
        public ActionResult FlickrAuth()
        {
            var f = new Flickr(FlickrGalleryModuleContext.Settings.FlickrApiKey, FlickrGalleryModuleContext.Settings.FlickrSharedSecret);
            var routeValues = new RouteValueDictionary();
            routeValues["controller"] = "Flickr";
            routeValues["action"] = "Auth";
            var url = ModuleRoutingProvider.Instance().GenerateUrl(routeValues, ModuleContext);
            OAuthRequestToken token = f.OAuthGetRequestToken(url);
            Session["RequestToken"] = token;
            var redirectUrl = f.OAuthCalculateAuthorizationUrl(token.Token, AuthLevel.Write);
            return Redirect(redirectUrl);
        }
    }
}
