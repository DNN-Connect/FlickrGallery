using System;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.FlickrGallery.Core.Models.Albums;
using System.Linq;

namespace Connect.FlickrGallery.Core.Repositories
{
    public class AlbumRepository : ServiceLocator<IAlbumRepository, AlbumRepository>, IAlbumRepository
    {
        protected override Func<IAlbumRepository> GetFactory()
        {
            return () => new AlbumRepository();
        }
        public Dictionary<string, Album> GetAlbumsDictionary(int moduleId)
        {
            return GetAlbums(moduleId).ToDictionary(a => a.PhotosetId, a => a);
        }
        public IEnumerable<Album> GetAlbums(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Album>();
                return rep.Get(moduleId);
            }
        }
        public Album GetAlbum(int moduleId, int albumId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Album>();
                return rep.GetById(albumId, moduleId);
            }
        }
        public int AddAlbum(ref AlbumBase album)
        {
            Requires.NotNull(album);
            Requires.PropertyNotNegative(album, "ModuleId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumBase>();
                rep.Insert(album);
            }
            return album.AlbumId;
        }
        public void DeleteAlbum(AlbumBase album)
        {
            Requires.NotNull(album);
            Requires.PropertyNotNegative(album, "AlbumId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumBase>();
                rep.Delete(album);
            }
        }
        public void DeleteAlbum(int moduleId, int albumId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumBase>();
                rep.Delete("WHERE ModuleId = @0 AND AlbumId = @1", moduleId, albumId);
            }
        }
        public void UpdateAlbum(AlbumBase album)
        {
            Requires.NotNull(album);
            Requires.PropertyNotNegative(album, "AlbumId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<AlbumBase>();
                rep.Update(album);
            }
        }
    }

    public interface IAlbumRepository
    {
        Dictionary<string, Album> GetAlbumsDictionary(int moduleId);
        IEnumerable<Album> GetAlbums(int moduleId);
        Album GetAlbum(int moduleId, int albumId);
        int AddAlbum(ref AlbumBase album);
        void DeleteAlbum(AlbumBase album);
        void DeleteAlbum(int moduleId, int albumId);
        void UpdateAlbum(AlbumBase album);
    }
}