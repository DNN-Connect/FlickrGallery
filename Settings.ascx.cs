using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using Connect.DNN.Modules.FlickrGallery.Common;
using FlickrNet;
using DotNetNuke.Web.Mvc.Routing;
using System.Web.Routing;

namespace Connect.DNN.Modules.FlickrGallery
{
    public partial class Settings : ModuleSettingsBase
    {
        #region Properties
        private ModuleSettings _settings;
        public ModuleSettings ModSettings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Common.ModuleSettings.GetSettings(ModuleConfiguration);
                }
                return _settings;
            }
            set { _settings = value; }
        }
        #endregion

        #region Base Method Implementations
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    txtFlickrApiKey.Text = ModSettings.FlickrApiKey;
                    txtFlickrSharedSecret.Text = ModSettings.FlickrSharedSecret;
                    txtFlickrGroupId.Text = ModSettings.FlickrGroupId;
                    txtFlickrUserId.Text = ModSettings.FlickrUserId;
                    txtFlickrAlbumId.Text = ModSettings.FlickrAlbumId;
                    chkIncludeInService.Checked = ModSettings.IncludeInService;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                ModSettings.FlickrApiKey = txtFlickrApiKey.Text.Trim();
                ModSettings.FlickrSharedSecret = txtFlickrSharedSecret.Text.Trim();
                ModSettings.FlickrGroupId = txtFlickrGroupId.Text.Trim();
                ModSettings.FlickrUserId = txtFlickrUserId.Text.Trim();
                ModSettings.FlickrAlbumId = txtFlickrAlbumId.Text.Trim();
                ModSettings.IncludeInService = chkIncludeInService.Checked;
                ModSettings.SaveSettings(ModuleConfiguration);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}