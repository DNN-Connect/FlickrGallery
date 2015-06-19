
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace Connect.DNN.Modules.FlickrGallery.Models.Photographers
{
    [TableName("Connect_FlickrGallery_Photographers")]
    [PrimaryKey("PhotographerId", AutoIncrement = true)]
    [DataContract]
    [Scope("ModuleId")]
    public partial class PhotographerBase
    {

        #region " Public Properties "
        [DataMember()]
        public int PhotographerId { get; set; }
        [DataMember()]
        public int ModuleId { get; set; }
        [DataMember()]
        public string FlickrId { get; set; }
        [DataMember()]
        public string OwnerName { get; set; }
        [DataMember()]
        public int? UserId { get; set; }
        #endregion

        #region " Methods "
        public void ReadPhotographerBase(PhotographerBase photographer)
        {
            if (photographer.PhotographerId > -1)
                PhotographerId = photographer.PhotographerId;

            if (photographer.ModuleId > -1)
                ModuleId = photographer.ModuleId;

            if (!String.IsNullOrEmpty(photographer.FlickrId))
                FlickrId = photographer.FlickrId;

            if (!String.IsNullOrEmpty(photographer.OwnerName))
                OwnerName = photographer.OwnerName;

            if (photographer.UserId > -1)
                UserId = photographer.UserId;

        }
        #endregion

    }
}



