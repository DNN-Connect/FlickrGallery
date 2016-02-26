using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.FlickrGallery.Core.Models.AlbumPhotos
{

    [TableName("vw_Connect_FlickrGallery_AlbumPhotos")]
    [DataContract]
    public partial class AlbumPhoto : AlbumPhotoBase
    {

        #region .ctor
        public AlbumPhoto() : base()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string FlickrId { get; set; }
        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public int PhotographerId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime DateTaken { get; set; }
        [DataMember]
        public DateTime? DateAddedToGroup { get; set; }
        [DataMember]
        public string LargeSquareThumbnailUrl { get; set; }
        [DataMember]
        public int? LargeHeight { get; set; }
        [DataMember]
        public int? LargeWidth { get; set; }
        [DataMember]
        public string LargeUrl { get; set; }
        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string PhotographerFlickrId { get; set; }
        #endregion

        #region Methods
        public AlbumPhotoBase GetAlbumPhotoBase()
        {
            AlbumPhotoBase res = new AlbumPhotoBase();
            res.AlbumId = AlbumId;
            res.PhotoId = PhotoId;
            return res;
        }
        #endregion

    }
}
