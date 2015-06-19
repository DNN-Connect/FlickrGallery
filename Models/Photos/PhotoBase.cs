
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
        public string SquareThumbnailUrl { get; set; }
        [DataMember()]
        public int? Medium800Height { get; set; }
        [DataMember()]
        public int? Medium800Width { get; set; }
        [DataMember()]
        public string Medium800Url { get; set; }
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

            if (!String.IsNullOrEmpty(photo.SquareThumbnailUrl))
                SquareThumbnailUrl = photo.SquareThumbnailUrl;

            if (photo.Medium800Height > -1)
                Medium800Height = photo.Medium800Height;

            if (photo.Medium800Width > -1)
                Medium800Width = photo.Medium800Width;

            if (!String.IsNullOrEmpty(photo.Medium800Url))
                Medium800Url = photo.Medium800Url;

        }
        #endregion

    }
}



