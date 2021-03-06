using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.FlickrGallery.Core.Models.Photos
{

    [TableName("vw_Connect_FlickrGallery_Photos")]
    [PrimaryKey("PhotoId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class Photo : PhotoBase
    {

        #region .ctor
        public Photo() : base()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string PhotographerFlickrId { get; set; }
        #endregion

        #region Methods
        public PhotoBase GetPhotoBase()
        {
            PhotoBase res = new PhotoBase();
            res.PhotoId = PhotoId;
            res.FlickrId = FlickrId;
            res.ModuleId = ModuleId;
            res.PhotographerId = PhotographerId;
            res.Title = Title;
            res.DateTaken = DateTaken;
            res.DateAddedToGroup = DateAddedToGroup;
            res.LargeSquareThumbnailUrl = LargeSquareThumbnailUrl;
            res.LargeHeight = LargeHeight;
            res.LargeWidth = LargeWidth;
            res.LargeUrl = LargeUrl;
            return res;
        }
        #endregion

    }
}
