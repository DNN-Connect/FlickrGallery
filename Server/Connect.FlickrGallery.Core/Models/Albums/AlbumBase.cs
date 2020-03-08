using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
namespace Connect.FlickrGallery.Core.Models.Albums
{
    [TableName("Connect_FlickrGallery_Albums")]
    [PrimaryKey("AlbumId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class AlbumBase
    {

        #region .ctor
        public AlbumBase()
        {
            AlbumId = -1;
        }
        #endregion

        #region Properties
        [DataMember]
        public int AlbumId { get; set; }
        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public string PhotosetId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int? PrimaryPhotoId { get; set; }
        #endregion

        #region Methods
        public void ReadAlbumBase(AlbumBase album)
        {
            if (album.AlbumId > -1)
                AlbumId = album.AlbumId;

            if (album.ModuleId > -1)
                ModuleId = album.ModuleId;

            if (!String.IsNullOrEmpty(album.PhotosetId))
                PhotosetId = album.PhotosetId;

            if (!String.IsNullOrEmpty(album.Title))
                Title = album.Title;

            if (album.PrimaryPhotoId > -1)
                PrimaryPhotoId = album.PrimaryPhotoId;

        }
        #endregion

    }
}



