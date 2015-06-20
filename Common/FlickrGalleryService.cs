
using System;
using System.Collections.Generic;
using System.Text;
using Connect.DNN.Modules.FlickrGallery.Common.Settings;
using Connect.DNN.Modules.FlickrGallery.Controllers;
using Connect.DNN.Modules.FlickrGallery.Models.Photographers;
using Connect.DNN.Modules.FlickrGallery.Models.Photos;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Scheduling;
using FlickrNet;
using Photo = FlickrNet.Photo;

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
                    if (!string.IsNullOrEmpty(settings.FlickrApiKey) && !string.IsNullOrEmpty(settings.FlickrGroupId))
                    {
                        log.AppendFormat("Synchronizing module '{0}' - Module ID {1}<br />", mod.ModuleTitle, mod.ModuleID);
                        Dictionary<string, Photographer> photographers =
                            PhotographersController.GetPhotographers(mod.ModuleID);
                        Dictionary<string, Models.Photos.Photo> existingPics = PhotosController.GetPhotos(mod.ModuleID);
                        List<string> flickrList = new List<string>();
                        Flickr flickr = new Flickr(settings.FlickrApiKey);
                        flickr.InstanceCacheDisabled = true;
                        PhotoCollection photos;
                        int added = 0;
                        int deleted = 0;
                        int page = 1;
                        do
                        {
                            photos = flickr.GroupsPoolsGetPhotos(settings.FlickrGroupId, null, null, PhotoSearchExtras.All, page, 100);
                            foreach (Photo photo in photos)
                            {
                                flickrList.Add(photo.PhotoId);
                                if (!existingPics.ContainsKey(photo.PhotoId))
                                {
                                    if (!photographers.ContainsKey(photo.UserId))
                                    {
                                        PhotographerBase ph = new PhotographerBase();
                                        ph.ModuleId = mod.ModuleID;
                                        ph.FlickrId = photo.UserId;
                                        ph.OwnerName = photo.OwnerName;
                                        // todo: check if they are a user
                                        PhotographersController.AddPhotographer(ref ph);
                                        photographers = PhotographersController.GetPhotographers(mod.ModuleID);
                                    }
                                    var photographer = photographers[photo.UserId];
                                    PhotoBase p = new PhotoBase();
                                    p.ModuleId = mod.ModuleID;
                                    p.FlickrId = photo.PhotoId;
                                    p.PhotographerId = photographer.PhotographerId;
                                    p.DateAddedToGroup = photo.DateAddedToGroup;
                                    p.DateTaken = photo.DateTaken;
                                    p.LargeHeight = photo.LargeHeight;
                                    p.LargeUrl = photo.LargeUrl;
                                    p.LargeWidth = photo.LargeWidth;
                                    p.LargeSquareThumbnailUrl = photo.LargeSquareThumbnailUrl;
                                    PhotosController.AddPhoto(ref p);
                                    added += 1;
                                }
                            }
                            page += 1;
                        } while (photos.Count == 100);

                        foreach (var p in existingPics.Values)
                        {
                            if (!flickrList.Contains(p.FlickrId))
                            {
                                PhotosController.DeletePhoto(p);
                                deleted += 1;
                            }
                        }
                        log.AppendFormat("Synchronized Module ID {0}: ", mod.ModuleID);
                        log.AppendFormat("Added {0} photos and deleted {1}<br />", added, deleted);
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