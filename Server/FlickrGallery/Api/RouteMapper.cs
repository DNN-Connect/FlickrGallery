using DotNetNuke.Web.Api;

namespace Connect.DNN.Modules.FlickrGallery.Api
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Connect/FlickrGallery", "FlickrGalleryMap1", "{controller}/{action}", null, null, new[] { "Connect.DNN.Modules.FlickrGallery.Api" });
            mapRouteManager.MapHttpRoute("Connect/FlickrGallery", "FlickrGalleryMap2", "{controller}/{action}/{id}", null, new { id = "\\d*" }, new[] { "Connect.DNN.Modules.FlickrGallery.Api" });
        }
    }
}