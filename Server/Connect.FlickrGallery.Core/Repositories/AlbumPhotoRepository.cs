using System;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.FlickrGallery.Core.Models.AlbumPhotos;
using DotNetNuke.Collections;

namespace Connect.FlickrGallery.Core.Repositories
{
    public class AlbumPhotoRepository : ServiceLocator<IAlbumPhotoRepository, AlbumPhotoRepository>, IAlbumPhotoRepository
    {
        protected override Func<IAlbumPhotoRepository> GetFactory()
        {
            return () => new AlbumPhotoRepository();
        }
        public IPagedList<AlbumPhoto> GetPage(int moduleId, int albumId, int pageIndex, int pageSize)
        {
            return GetPage(moduleId, albumId, "DateTaken", pageIndex, pageSize);
        }
        public IPagedList<AlbumPhoto> GetPage(int moduleId, int albumId, string orderByField, int pageIndex, int pageSize)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumPhoto>();
                IPagedList<AlbumPhoto> res = rep.Find(pageIndex, pageSize, "WHERE ModuleId = @0 AND AlbumId = @1 ORDER BY " + orderByField, moduleId, albumId);
                return res;
            }
        }
        public IEnumerable<AlbumPhoto> GetAlbumPhotosByAlbum(int albumId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<AlbumPhoto>(System.Data.CommandType.Text,
                    "SELECT * FROM {databaseOwner}{objectQualifier}vw_Connect_FlickrGallery_AlbumPhotos WHERE AlbumId=@0",
                    albumId);
            }
        }
        public void SetAlbumPhoto(int albumId, int photoId)
        {
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "IF NOT EXISTS (SELECT * FROM {databaseOwner}{objectQualifier}Connect_FlickrGallery_AlbumPhotos " +
                    "WHERE AlbumId=@0 AND PhotoId=@1) " +
                    "INSERT INTO {databaseOwner}{objectQualifier}Connect_FlickrGallery_AlbumPhotos (AlbumId, PhotoId) " +
                    "SELECT @0, @1", albumId, photoId);
            }
        }
        public void SetAlbumPhotos(int albumId, List<int> albumPhotos)
        {

            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "DELETE FROM {databaseOwner}{objectQualifier}Connect_FlickrGallery_AlbumPhotos WHERE AlbumId=@0", albumId);
                context.Execute(System.Data.CommandType.Text,
                    "INSERT INTO {databaseOwner}{objectQualifier}Connect_FlickrGallery_AlbumPhotos (AlbumId, PhotoId) " +
                    "SELECT @0, s.RecordID " +
                    "FROM {databaseOwner}{objectQualifier}SplitDelimitedIDs(@1, ',') s", albumId, string.Join(",", albumPhotos));
            }
        }
        public void DeleteAlbumPhoto(AlbumPhotoBase albumPhoto)
        {
            Requires.NotNull(albumPhoto);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumPhotoBase>();
                rep.Delete(albumPhoto);
            }
        }
        public void DeleteAlbumPhotosByAlbum(int albumId)
        {
            Requires.NotNull(albumId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumPhotoBase>();
                rep.Delete("WHERE AlbumId=@0", albumId);
            }
        }
    }

    public interface IAlbumPhotoRepository
    {
        IPagedList<AlbumPhoto> GetPage(int moduleId, int albumId, int pageIndex, int pageSize);
        IPagedList<AlbumPhoto> GetPage(int moduleId, int albumId, string orderByField, int pageIndex, int pageSize);
        IEnumerable<AlbumPhoto> GetAlbumPhotosByAlbum(int albumId);
        void SetAlbumPhoto(int albumId, int photoId);
        void SetAlbumPhotos(int albumId, List<int> albumPhotos);
        void DeleteAlbumPhoto(AlbumPhotoBase albumPhoto);
        void DeleteAlbumPhotosByAlbum(int albumId);
    }
}

