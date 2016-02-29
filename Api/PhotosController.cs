using Connect.DNN.Modules.FlickrGallery.Common;
using Connect.FlickrGallery.Core.Models.Albums;
using Connect.FlickrGallery.Core.Repositories;
using FlickrNet;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Connect.DNN.Modules.FlickrGallery.Api
{
    public class PhotosController : FlickrGalleryApiController
    {

        [HttpGet()]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Page(int id, int albumId)
        {
            switch (FlickrGalleryModuleContext.Settings.ViewType)
            {
                case ModuleSettings.ViewTypes.Album:
                    break;
                case ModuleSettings.ViewTypes.Group:
                    return Request.CreateResponse(HttpStatusCode.OK, PhotoRepository.Instance.GetPage(ActiveModule.ModuleID, id, 50));
                case ModuleSettings.ViewTypes.None:
                    break;
                case ModuleSettings.ViewTypes.User:
                    if (albumId == -1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, AlbumPhotoRepository.Instance.GetPage(ActiveModule.ModuleID, albumId, id, 50));
                    }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        public class AddedFile
        {
            public string fileName { get; set; }
            public string newName { get; set; }
            public bool sent { get; set; } = false;
            public string albumName { get; set; }
        }

        [HttpPost]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Add)]
        public HttpResponseMessage SaveUploadedFile()
        {
            var res = new List<AddedFile>();
            try
            {
                foreach (string fileName in HttpContext.Current.Request.Files)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        var tmpImgDir = string.Format("{0}\\Connect\\FlickrGallery\\Temp\\{1}", PortalSettings.HomeDirectoryMapPath, ActiveModule.ModuleID);
                        if (!Directory.Exists(tmpImgDir)) Directory.CreateDirectory(tmpImgDir);
                        var fName = string.Format("{0:yyyy-MM-dd-HH-mm-ss-ffffff}{1}", DateTime.Now, Path.GetExtension(file.FileName));
                        var path = string.Format("{0}\\{1}", tmpImgDir, fName);
                        file.SaveAs(path);
                        res.Add(new AddedFile() { fileName = file.FileName, newName = fName });
                    }
                }
            }
            catch (Exception ex)
            {
                return ServiceError(ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPost]
        [FlickrGalleryAuthorize(SecurityLevel = SecurityAccessLevel.Add)]
        public HttpResponseMessage Send([FromBody]AddedFile data)
        {
            var tmpImgDir = string.Format("{0}\\Connect\\FlickrGallery\\Temp\\{1}", PortalSettings.HomeDirectoryMapPath, ActiveModule.ModuleID);
            var path = string.Format("{0}\\{1}", tmpImgDir, data.newName);
            if (File.Exists(path))
            {
                Flickr flickr = new Flickr(FlickrGalleryModuleContext.Settings.FlickrApiKey, FlickrGalleryModuleContext.Settings.FlickrSharedSecret);
                flickr.OAuthAccessToken = FlickrGalleryModuleContext.Settings.OAuthAccessToken;
                flickr.OAuthAccessTokenSecret = FlickrGalleryModuleContext.Settings.OAuthAccessTokenSecret;
                flickr.InstanceCacheDisabled = true;
                var photoId = flickr.UploadPicture(path, data.fileName);
                if (!string.IsNullOrEmpty(data.albumName))
                {
                    var album = AlbumRepository.Instance.GetAlbums(ActiveModule.ModuleID).FirstOrDefault(a => a.Title == data.albumName);
                    if (album == null)
                    {
                        var newSet = flickr.PhotosetsCreate(data.albumName, photoId);
                        var a = new AlbumBase() { PhotosetId = newSet.PhotosetId, ModuleId = ActiveModule.ModuleID, Title = newSet.Title };
                        AlbumRepository.Instance.AddAlbum(ref a);
                    }
                    else
                    {
                        flickr.PhotosetsAddPhoto(album.PhotosetId, photoId);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, photoId);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

    }
}