using System.Web.WebPages;
using Connect.DNN.Modules.FlickrGallery.Common.Settings;
using DotNetNuke.Web.Razor;
using DotNetNuke.Web.Razor.Helpers;

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

        public new DnnHelper Dnn
        {
            get
            {
                return base.Dnn;
            }
        }
        public new HtmlHelper Html
        {
            get
            {
                return base.Html;
            }
        }
        public new UrlHelper Url
        {
            get
            {
                return base.Url;
            }
        }
    }
    public abstract class FlickrGalleryWebPage<TModel> : DotNetNukeWebPage<TModel> where TModel : class
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

        protected override void ConfigurePage(WebPageBase parentPage)
        {
            base.ConfigurePage(parentPage);
            Context = parentPage.Context;
            FlickrGalleryWebPage parent = (FlickrGalleryWebPage)parentPage;
            _dnn = parent.Dnn;
            _html = parent.Html;
            _url = parent.Url;
        }
        private DnnHelper _dnn;
        public new DnnHelper Dnn
        {
            get
            {
                if (base.Dnn == null)
                {
                    return _dnn;
                }
                else
                {
                    return base.Dnn;
                }
            }
        }

        private HtmlHelper _html;
        public new HtmlHelper Html
        {
            get
            {
                if (base.Html == null)
                {
                    return _html;
                }
                else
                {
                    return base.Html;
                }
            }
        }

        private UrlHelper _url;
        public new UrlHelper Url
        {
            get
            {
                if (base.Url == null)
                {
                    return _url;
                }
                else
                {
                    return base.Url;
                }
            }
        }

    }
}