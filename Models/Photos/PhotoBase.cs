
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.DNN.Modules.FlickrGallery.Models.Photos
{
    [TableName("Connect_FlickrGallery_Photos")]
    [PrimaryKey("PhotoId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class PhotoBase
    {

        #region " Public Properties "
        [DataMember()]
        public int PhotoId { get; set; }
        [DataMember()]
        public string FlickrId { get; set; }
        [DataMember()]
        public int ModuleId { get; set; }
        [DataMember()]
        public int PhotographerId { get; set; }
        [DataMember()]
        public string Title { get; set; }
        [DataMember()]
        public DateTime DateTaken { get; set; }
        [DataMember()]
        public DateTime? DateAddedToGroup { get; set; }
        [DataMember()]
        public string LargeSquareThumbnailUrl { get; set; }
        [DataMember()]
        public int? LargeHeight { get; set; }
        [DataMember()]
        public int? LargeWidth { get; set; }
        [DataMember()]
        public string LargeUrl { get; set; }
        #endregion

        #region " Methods "
        public void ReadPhotoBase(PhotoBase photo)
        {
            if (photo.PhotoId > -1)
                PhotoId = photo.PhotoId;

            if (!String.IsNullOrEmpty(photo.FlickrId))
                FlickrId = photo.FlickrId;

            if (photo.ModuleId > -1)
                ModuleId = photo.ModuleId;

            if (photo.PhotographerId > -1)
                PhotographerId = photo.PhotographerId;

            if (!String.IsNullOrEmpty(photo.Title))
                Title = photo.Title;

            DateTaken = photo.DateTaken;

            if (photo.DateAddedToGroup != null)
            DateAddedToGroup = photo.DateAddedToGroup;

            if (!String.IsNullOrEmpty(photo.LargeSquareThumbnailUrl))
                LargeSquareThumbnailUrl = photo.LargeSquareThumbnailUrl;

            if (photo.LargeHeight > -1)
                LargeHeight = photo.LargeHeight;

            if (photo.LargeWidth > -1)
                LargeWidth = photo.LargeWidth;

            if (!String.IsNullOrEmpty(photo.LargeUrl))
                LargeUrl = photo.LargeUrl;

        }
        #endregion

    }
}



