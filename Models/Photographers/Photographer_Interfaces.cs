
using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Photographers
{

 [Serializable(), XmlRoot("Photographer")]
 public partial class Photographer
 {

  #region IHydratable
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   DisplayName = Convert.ToString(Null.SetNull(dr["DisplayName"], DisplayName));
   Username = Convert.ToString(Null.SetNull(dr["Username"], Username));
   Email = Convert.ToString(Null.SetNull(dr["Email"], Email));
  }
  #endregion

  #region IPropertyAccess
  public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
    case "displayname": // NVarChar
     if (DisplayName == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(DisplayName, strFormat);
    case "username": // NVarChar
     if (Username == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Username, strFormat);
    case "email": // NVarChar
     if (Email == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Email, strFormat);
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }
  }
  #endregion

 }
}

