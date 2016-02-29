using System.Web.Mvc;
using Connect.DNN.Modules.FlickrGallery.Common;
using FlickrNet;

namespace Connect.DNN.Modules.FlickrGallery.Controllers
{
    public class FlickrController : FlickrGalleryMvcController
    {
        [HttpGet]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Anonymous)]
        public ActionResult Auth()
        {
            var f = new Flickr(FlickrGalleryModuleContext.Settings.FlickrApiKey, FlickrGalleryModuleContext.Settings.FlickrSharedSecret);
            OAuthRequestToken requestToken = Session["RequestToken"] as OAuthRequestToken;
            try
            {
                OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, Request.QueryString["oauth_verifier"]);
                FlickrGalleryModuleContext.Settings.OAuthAccessToken = accessToken.Token;
                FlickrGalleryModuleContext.Settings.OAuthAccessTokenSecret = accessToken.TokenSecret;
                FlickrGalleryModuleContext.Settings.SaveSettings(ModuleContext.Configuration);
            }
            catch (OAuthException ex)
            {
            }
            return RedirectToDefaultRoute();
        }

    }
}