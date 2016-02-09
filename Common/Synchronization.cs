using System.Linq;
using System.Collections.Generic;
using System.Text;
using FlickrNet;
using Photo = FlickrNet.Photo;
using Connect.FlickrGallery.Core.Models.Photographers;
using Connect.FlickrGallery.Core.Repositories;
using Connect.FlickrGallery.Core.Models.Albums;

namespace Connect.DNN.Modules.FlickrGallery.Common
{
    public class Synchronization
    {

        public static string SyncUser(ModuleSettings settings, int moduleId)
        {
            var log = new StringBuilder();
            Flickr flickr = new Flickr(settings.FlickrApiKey);
            flickr.InstanceCacheDisabled = true;
            // download albums
            Dictionary<string, Photographer> photographers = PhotographerRepository.Instance.GetPhotographersDictionary(moduleId);
            Dictionary<string, Album> existingAlbums = AlbumRepository.Instance.GetAlbumsDictionary(moduleId);
            Dictionary<string, Connect.FlickrGallery.Core.Models.Photos.Photo> existingPics = PhotoRepository.Instance.GetPhotos(moduleId);
            var addedAlbums = 0;
            var deletedAlbums = 0;
            var addedPhotos = 0;
            var albumsToAdd = new Dictionary<int, Photoset>();
            var albumsAdded = new Dictionary<int, AlbumBase>();
            var flickrAlbums = flickr.PhotosetsGetList(settings.FlickrUserId);
            var flickrAlbumList = new List<string>();
            foreach (var flickrAlbum in flickrAlbums)
            {
                flickrAlbumList.Add(flickrAlbum.PhotosetId);
                var albumId = -1;
                if (!existingAlbums.ContainsKey(flickrAlbum.PhotosetId))
                {
                    var album = new AlbumBase() { PhotosetId = flickrAlbum.PhotosetId, ModuleId = moduleId, Title = flickrAlbum.Title };
                    AlbumRepository.Instance.AddAlbum(ref album);
                    albumId = album.AlbumId;
                    albumsToAdd.Add(albumId, flickrAlbum);
                    albumsAdded.Add(albumId, album);
                    addedAlbums += 1;
                }
                else
                {
                    albumId = existingAlbums[flickrAlbum.PhotosetId].AlbumId;
                }
                var flickrPhotos = flickr.PhotosetsGetPhotos(flickrAlbum.PhotosetId, PhotoSearchExtras.All);
                var albumPhotoIds = new List<int>();
                foreach (var flickrPhoto in flickrPhotos)
                {

                    if (!existingPics.ContainsKey(flickrPhoto.PhotoId))
                    {
                        if (!photographers.ContainsKey(flickrPhoto.UserId))
                        {
                            PhotographerBase ph = new PhotographerBase();
                            ph.ModuleId = moduleId;
                            ph.FlickrId = flickrPhoto.UserId;
                            ph.OwnerName = flickrPhoto.OwnerName;
                            // todo: check if they are a user
                            PhotographerRepository.Instance.AddPhotographer(ref ph);
                            photographers = PhotographerRepository.Instance.GetPhotographersDictionary(moduleId);
                        }
                        var photographer = photographers[flickrPhoto.UserId];
                        Connect.FlickrGallery.Core.Models.Photos.PhotoBase p = new Connect.FlickrGallery.Core.Models.Photos.PhotoBase();
                        p.ModuleId = moduleId;
                        p.FlickrId = flickrPhoto.PhotoId;
                        p.PhotographerId = photographer.PhotographerId;
                        p.DateAddedToGroup = flickrPhoto.DateAddedToGroup;
                        p.DateTaken = flickrPhoto.DateTaken;
                        p.LargeHeight = flickrPhoto.LargeHeight;
                        p.LargeUrl = flickrPhoto.LargeUrl;
                        p.LargeWidth = flickrPhoto.LargeWidth;
                        p.LargeSquareThumbnailUrl = flickrPhoto.LargeSquareThumbnailUrl;
                        PhotoRepository.Instance.AddPhoto(ref p);
                        existingPics.Add(flickrPhoto.PhotoId, new Connect.FlickrGallery.Core.Models.Photos.Photo() { PhotoId = p.PhotoId, FlickrId = p.FlickrId });
                        addedPhotos += 1;
                        albumPhotoIds.Add(p.PhotoId);
                    }
                    else
                    {
                        albumPhotoIds.Add(existingPics[flickrPhoto.PhotoId].PhotoId);
                    }
                }
                AlbumPhotoRepository.Instance.SetAlbumPhotos(albumId, albumPhotoIds);
            }
            // check for albums to delete
            var albumsToDelete = new List<int>();
            foreach (var album in existingAlbums.Values)
            {
                if (!flickrAlbumList.Contains(album.PhotosetId))
                {
                    albumsToDelete.Add(album.AlbumId);
                }
            }
            foreach (int albumId in albumsToDelete)
            {
                AlbumRepository.Instance.DeleteAlbum(moduleId, albumId);
                deletedAlbums += 1;
            }
            // update primary pictures
            foreach (var album in AlbumRepository.Instance.GetAlbums(moduleId))
            {
                var fa = flickrAlbums.FirstOrDefault(a => a.PhotosetId == album.PhotosetId);
                if (fa != null)
                {
                    if (existingPics.ContainsKey(fa.PrimaryPhotoId))
                    {
                        if (album.PrimaryPhotoId != existingPics[fa.PrimaryPhotoId].PhotoId)
                        {
                            album.PrimaryPhotoId = existingPics[fa.PrimaryPhotoId].PhotoId;
                            AlbumRepository.Instance.UpdateAlbum(album.GetAlbumBase());
                        }
                    }
                }
            }
            log.AppendFormat("Synchronized Module ID {0}: ", moduleId);
            log.AppendFormat("Added {0} albums and deleted {1}<br />", addedAlbums, deletedAlbums);
            log.AppendFormat("Added {0} photos<br />", addedPhotos);
            return log.ToString();
        }

        public static string SyncGroup(ModuleSettings settings, int moduleId)
        {
            var log = new StringBuilder();
            Flickr flickr = new Flickr(settings.FlickrApiKey);
            flickr.InstanceCacheDisabled = true;
            // download group
            Dictionary<string, Photographer> photographers = PhotographerRepository.Instance.GetPhotographersDictionary(moduleId);
            Dictionary<string, Connect.FlickrGallery.Core.Models.Photos.Photo> existingPics = PhotoRepository.Instance.GetPhotos(moduleId);
            List<string> flickrList = new List<string>();
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
                            ph.ModuleId = moduleId;
                            ph.FlickrId = photo.UserId;
                            ph.OwnerName = photo.OwnerName;
                            // todo: check if they are a user
                            PhotographerRepository.Instance.AddPhotographer(ref ph);
                            photographers = PhotographerRepository.Instance.GetPhotographersDictionary(moduleId);
                        }
                        var photographer = photographers[photo.UserId];
                        Connect.FlickrGallery.Core.Models.Photos.PhotoBase p = new Connect.FlickrGallery.Core.Models.Photos.PhotoBase();
                        p.ModuleId = moduleId;
                        p.FlickrId = photo.PhotoId;
                        p.PhotographerId = photographer.PhotographerId;
                        p.DateAddedToGroup = photo.DateAddedToGroup;
                        p.DateTaken = photo.DateTaken;
                        p.LargeHeight = photo.LargeHeight;
                        p.LargeUrl = photo.LargeUrl;
                        p.LargeWidth = photo.LargeWidth;
                        p.LargeSquareThumbnailUrl = photo.LargeSquareThumbnailUrl;
                        PhotoRepository.Instance.AddPhoto(ref p);
                        added += 1;
                    }
                }
                page += 1;
            } while (photos.Count == 100);

            foreach (var p in existingPics.Values)
            {
                if (!flickrList.Contains(p.FlickrId))
                {
                    PhotoRepository.Instance.DeletePhoto(p);
                    deleted += 1;
                }
            }
            log.AppendFormat("Synchronized Module ID {0}: ", moduleId);
            log.AppendFormat("Added {0} photos and deleted {1}<br />", added, deleted);
            return log.ToString();
        }
    }
}