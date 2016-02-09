using DotNetNuke.Web.Api;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class FlickrGalleryApiController : DnnApiController
    {
        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = ModuleSettings.GetSettings(ActiveModule);
                }
                return _settings;
            }
            set { _settings = value; }
        }

    }
}