using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Settings;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class ModuleSettings
    {
        [ModuleSetting]
        public string FlickrApiKey { get; set; } = "";
        [ModuleSetting]
        public string FlickrSharedSecret { get; set; } = "";
        [ModuleSetting]
        public string FlickrGroupId { get; set; } = "";
        [ModuleSetting]
        public string FlickrUserId { get; set; } = "";
        [ModuleSetting]
        public string FlickrAlbumId { get; set; } = "";
        [ModuleSetting]
        public int ThumbnailSize { get; set; } = 100;
        [ModuleSetting]
        public int ZoomSize { get; set; } = 800;
        [ModuleSetting]
        public bool IncludeInService { get; set; } = true;
        [ModuleSetting]
        public string OAuthAccessToken { get; set; } = "";
        [ModuleSetting]
        public string OAuthAccessTokenSecret { get; set; } = "";

        public ViewTypes ViewType { get; set; } = ViewTypes.None;

        public static ModuleSettings GetSettings(ModuleInfo module)
        {
            var repo = new ModuleSettingsRepository();
            var res = repo.GetSettings(module);
            if (!string.IsNullOrEmpty(res.FlickrApiKey))
            {
                if (!string.IsNullOrEmpty(res.FlickrGroupId))
                {
                    res.ViewType = ViewTypes.Group;
                }
                else if (!string.IsNullOrEmpty(res.FlickrUserId))
                {
                    res.ViewType = ViewTypes.User;
                }
                else if (!string.IsNullOrEmpty(res.FlickrAlbumId))
                {
                    res.ViewType = ViewTypes.Album;
                }
            }
            return res;
        }

        public void SaveSettings(ModuleInfo module)
        {
            var repo = new ModuleSettingsRepository();
            repo.SaveSettings(module, this);
        }

        public enum ViewTypes
        {
            None = 0,
            Group = 1,
            User = 2,
            Album = 3
        }

    }
    public class ModuleSettingsRepository : SettingsRepository<ModuleSettings>
    {
    }
}