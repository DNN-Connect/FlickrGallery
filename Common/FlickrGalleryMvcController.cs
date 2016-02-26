using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class FlickrGalleryMvcController : DnnController
    {

        private ContextHelper _flickrgalleryModuleContext;
        public ContextHelper FlickrGalleryModuleContext
        {
            get { return _flickrgalleryModuleContext ?? (_flickrgalleryModuleContext = new ContextHelper(this)); }
        }

    }
}