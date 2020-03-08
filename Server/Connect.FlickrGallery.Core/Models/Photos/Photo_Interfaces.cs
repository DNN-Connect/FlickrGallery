using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Photos
{

    [Serializable(), XmlRoot("Photo")]
    public partial class Photo
    {

        #region IHydratable
        public override void Fill(IDataReader dr)
        {
            base.Fill(dr);
            OwnerName = Convert.ToString(Null.SetNull(dr["OwnerName"], OwnerName));
            PhotographerFlickrId = Convert.ToString(Null.SetNull(dr["PhotographerFlickrId"], PhotographerFlickrId));
        }
        #endregion

        #region IPropertyAccess
        public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
                case "ownername": // NVarChar
                    return PropertyAccess.FormatString(OwnerName, strFormat);
                case "photographerflickrid": // NVarChar
                    return PropertyAccess.FormatString(PhotographerFlickrId, strFormat);
                default:
                    return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
            }
        }
        #endregion

    }
}

