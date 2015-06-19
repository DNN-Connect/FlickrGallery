
using System;
using System.Data;
using System.Globalization;
using System.Xml.Serialization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Tokens;

namespace Connect.DNN.Modules.FlickrGallery.Models.Photographers
{

 [Serializable(), XmlRoot("Photographer")]
 public partial class Photographer
 {

  #region " IHydratable Implementation "
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   DisplayName = Convert.ToString(Null.SetNull(dr["DisplayName"], DisplayName));
   Username = Convert.ToString(Null.SetNull(dr["Username"], Username));
   Email = Convert.ToString(Null.SetNull(dr["Email"], Email));
  }
  #endregion

  #region " IPropertyAccess Implementation "
  public override string GetProperty(string strPropertyName, string strFormat, CultureInfo formatProvider, UserInfo accessingUser, Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
    case "displayname": // NVarChar
     if (DisplayName == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(DisplayName, strFormat);
    case "username": // NVarChar
     if (Username == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(Username, strFormat);
    case "email": // NVarChar
     if (Email == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(Email, strFormat);
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }

         return Null.NullString;
  }
  #endregion

 }
}

