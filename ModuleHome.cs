using System;
using DotNetNuke.Collections;
using Connect.DNN.Modules.FlickrGallery.Common;

namespace Connect.DNN.Modules.FlickrGallery
{
    public class ModuleHome : ModuleBase
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
            View = Request.QueryString.GetValueOrDefault<string>("View", Settings.View);
            AddService();
            LocalResourceFile = Globals.SharedResourceFileName;
        }

    }
}