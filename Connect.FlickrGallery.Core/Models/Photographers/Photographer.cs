using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.FlickrGallery.Core.Models.Photographers
{

    [TableName("vw_Connect_FlickrGallery_Photographers")]
    [PrimaryKey("PhotographerId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class Photographer : PhotographerBase
    {

        #region .ctor
        public Photographer() : base()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Email { get; set; }
        #endregion

        #region Methods
        public PhotographerBase GetPhotographerBase()
        {
            PhotographerBase res = new PhotographerBase();
            res.PhotographerId = PhotographerId;
            res.ModuleId = ModuleId;
            res.FlickrId = FlickrId;
            res.OwnerName = OwnerName;
            res.UserId = UserId;
            return res;
        }
        #endregion

    }
}
