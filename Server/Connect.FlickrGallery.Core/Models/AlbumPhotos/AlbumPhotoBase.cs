using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
namespace Connect.FlickrGallery.Core.Models.AlbumPhotos
{
    [TableName("Connect_FlickrGallery_AlbumPhotos")]
    [DataContract]
    public partial class AlbumPhotoBase
    {

        #region .ctor
        public AlbumPhotoBase()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public int AlbumId { get; set; }
        [DataMember]
        public int PhotoId { get; set; }
        #endregion

        #region Methods
        public void ReadAlbumPhotoBase(AlbumPhotoBase albumPhoto)
        {
            if (albumPhoto.AlbumId > -1)
                AlbumId = albumPhoto.AlbumId;

            if (albumPhoto.PhotoId > -1)
                PhotoId = albumPhoto.PhotoId;

        }
        #endregion

    }
}



