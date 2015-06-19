using DotNetNuke.Web.Razor;
using Connect.DNN.Modules.FlickrGallery.Common.Settings;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public abstract class FlickrGalleryWebPage : DotNetNukeWebPage
    {
        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = ModuleSettings.GetSettings(Dnn.Module);
                }
                return _settings;
            }
            set { _settings = value; }
        }
    }
    public abstract class FlickrGalleryWebPage<T> : DotNetNukeWebPage<T>
    {
        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = ModuleSettings.GetSettings(Dnn.Module);
                }
                return _settings;
            }
            set { _settings = value; }
        }
    }
}