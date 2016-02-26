using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.FlickrGallery.Core.Models.Albums
{

    [TableName("vw_Connect_FlickrGallery_Albums")]
    [PrimaryKey("AlbumId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class Album : AlbumBase
    {

        #region .ctor
        public Album() : base()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public int? LargeHeight { get; set; }
        [DataMember]
        public string LargeSquareThumbnailUrl { get; set; }
        [DataMember]
        public string LargeUrl { get; set; }
        [DataMember]
        public int? LargeWidth { get; set; }
        [DataMember]
        public int? NrPhotos { get; set; }
        #endregion

        #region Methods
        public AlbumBase GetAlbumBase()
        {
            AlbumBase res = new AlbumBase();
            res.AlbumId = AlbumId;
            res.ModuleId = ModuleId;
            res.PhotosetId = PhotosetId;
            res.Title = Title;
            res.PrimaryPhotoId = PrimaryPhotoId;
            return res;
        }
        #endregion

    }
}
