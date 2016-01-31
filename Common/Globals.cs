
namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public static class Globals
    {

        public const string SharedResourceFileName = "~/DesktopModules/Connect/FlickrGallery/App_LocalResources/SharedResources.resx";

    }
    public class ContentPage
    {
        public ContentPage(int pageNr)
        {
            PageNr = pageNr;
        }
        public int PageNr { get; set; }
    }
}