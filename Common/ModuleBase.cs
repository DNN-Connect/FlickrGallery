using Connect.DNN.Modules.FlickrGallery.Common.Settings;
using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.Razor;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class ModuleBase : RazorModuleBase
    {

        #region Properties
        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(ModuleContext.Configuration)); }
        }
        #endregion

        #region Public Methods
        public void AddService()
        {
            if (Context.Items["FlickrGalleryServiceAdded"] == null)
            {
                AddCssFile("photoswipe.css");
                AddCssFile("default-skin.css");
                JavaScript.RequestRegistration(CommonJs.DnnPlugins);
                ServicesFramework.Instance.RequestAjaxScriptSupport();
                ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                AddJavascriptFile("photoswipe.js", 70);
                AddJavascriptFile("photoswipe-ui-default.js", 70);
                AddJavascriptFile("connect.flickrgallery.js", 70);
                string script = "(function($){$(document).ready(function(){ galleryService = new GalleryService($, {}, " + ModuleContext.ModuleId + ") })})(jQuery);";
                Page.ClientScript.RegisterClientScriptBlock(script.GetType(), ID + "_service", script, true);
                Context.Items["FlickrGalleryServiceAdded"] = true;
            }

        }

        public void AddJavascriptFile(string jsFilename, int priority)
        {
            ClientResourceManager.RegisterScript(Page, ResolveUrl("~/DesktopModules/Connect/FlickrGallery/js/" + jsFilename) + "?_=" + Settings.Version, priority);
        }

        public void AddCssFile(string cssFileName)
        {
            ClientResourceManager.RegisterStyleSheet(Page, ResolveUrl("~/DesktopModules/Connect/FlickrGallery/css/" + cssFileName) + "?_=" + Settings.Version);
        }
        #endregion

    }
}