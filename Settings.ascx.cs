
using System;
using Connect.DNN.Modules.FlickrGallery.Common.Settings;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

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
                    _settings = Common.Settings.ModuleSettings.GetSettings(ModuleConfiguration);
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
                    txtFlickrGroupId.Text = ModSettings.FlickrGroupId;
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
                ModSettings.FlickrGroupId = txtFlickrGroupId.Text.Trim();
                ModSettings.SaveSettings();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}