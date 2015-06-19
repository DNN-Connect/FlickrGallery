
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.DNN.Modules.FlickrGallery.Models.Photographers
{

    [TableName("vw_Connect_FlickrGallery_Photographers")]
    [PrimaryKey("PhotographerId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class Photographer  : PhotographerBase 
    {

        #region " Private Members "
        #endregion

        #region " Constructors "
        public Photographer()  : base() 
        {
        }
        #endregion

        #region " Public Properties "
        [DataMember()]
        public string DisplayName { get; set; }
        [DataMember()]
        public string Username { get; set; }
        [DataMember()]
        public string Email { get; set; }
        #endregion

        #region " Public Methods "
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
