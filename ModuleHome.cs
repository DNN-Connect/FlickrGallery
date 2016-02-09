using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using Connect.DNN.Modules.FlickrGallery.Common;
using DotNetNuke.Security;

namespace Connect.DNN.Modules.FlickrGallery
{
    public class ModuleHome : ModuleBase, IActionable
    {
        public string View { get; set; }

        protected override string RazorScriptFile
        {
            get
            {
                return string.Format("~/DesktopModules/Connect/FlickrGallery/Views/{0}.cshtml", View);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            View = "Default";
            if (Settings.ViewType == ModuleSettings.ViewTypes.User)
            {
                if (Request.Params["AlbumId"] != null)
                {
                    View = "Album";
                }
                else
                {
                    View = "Albums";
                }
            }
            AddService();
            AddActionHandler(ModuleAction_Click);
            LocalResourceFile = Globals.SharedResourceFileName;
        }

        #region IActionable
        public ModuleActionCollection ModuleActions
        {
            get
            {
                // add the Edit Text action
                var Actions = new ModuleActionCollection();
                Actions.Add(ModuleContext.GetNextActionID(),
                            LocalizeString("Refresh"),
                            "action",
                            "refresh",
                            "",
                            "",
                            true,
                            SecurityAccessLevel.Edit,
                            true,
                            false);
                return Actions;
            }
        }

        private void ModuleAction_Click(object sender, ActionEventArgs e)
        {
            try
            {
                if (e.Action.CommandArgument == "refresh")
                {
                    switch (Settings.ViewType)
                    {
                        case ModuleSettings.ViewTypes.Album:
                            break;
                        case ModuleSettings.ViewTypes.Group:
                            Synchronization.SyncGroup(Settings, ModuleContext.ModuleId);
                            break;
                        case ModuleSettings.ViewTypes.User:
                            Synchronization.SyncUser(Settings, ModuleContext.ModuleId);
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        #endregion

        private void AddActionHandler(ActionEventHandler e)
        {
            DotNetNuke.UI.Skins.Skin ParentSkin = DotNetNuke.UI.Skins.Skin.GetParentSkin(this);
            if (ParentSkin != null)
            {
                ParentSkin.RegisterModuleActionEvent(ModuleContext.ModuleId, e);
            }
        }

    }
}