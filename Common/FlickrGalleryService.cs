using System;
using System.Collections.Generic;
using System.Text;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Scheduling;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class FlickrGalleryService : SchedulerClient
    {

        private StringBuilder log = new StringBuilder();

        public FlickrGalleryService(ScheduleHistoryItem historyItem)
        {
            ScheduleHistoryItem = historyItem;
        }

        public override void DoWork()
        {
            Progressing();

            try
            {
                List<ModuleInfo> modules =
    CBO.FillCollection<ModuleInfo>(
        DataProvider.Instance()
            .ExecuteSQL(
                "SELECT DISTINCT m.* FROM {databaseOwner}{objectQualifier}vw_Modules m INNER JOIN {databaseOwner}{objectQualifier}ModuleDefinitions md ON md.ModuleDefID=m.ModuleDefID INNER JOIN {databaseOwner}{objectQualifier}DesktopModules dm ON dm.DesktopModuleID=md.DesktopModuleID WHERE dm.ModuleName='Connect_FlickrGallery'"));

                foreach (var mod in modules)
                {
                    var settings = ModuleSettings.GetSettings(mod);
                    if (settings.IncludeInService)
                    {
                        log.AppendFormat("Synchronizing module '{0}' - Module ID {1}<br />", mod.ModuleTitle, mod.ModuleID);
                        switch (settings.ViewType)
                        {
                            case ModuleSettings.ViewTypes.Album:
                                break;
                            case ModuleSettings.ViewTypes.Group:
                                log.AppendLine(Synchronization.SyncGroup(settings, mod.ModuleID));
                                break;
                            case ModuleSettings.ViewTypes.User:
                                log.AppendLine(Synchronization.SyncUser(settings, mod.ModuleID));
                                break;
                        }
                    }
                }

                log.AppendLine("Finished");
                ScheduleHistoryItem.AddLogNote(log.ToString());
                ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                ScheduleHistoryItem.AddLogNote(log.ToString() + "<br />Scheduled task failed: " + ex.Message + "(" + ex.StackTrace + ")<br />");
                ScheduleHistoryItem.Succeeded = false;
                Errored(ref ex);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }
    }
}