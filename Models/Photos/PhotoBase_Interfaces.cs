
using System;
using System.Data;
using System.Globalization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Tokens;

namespace Connect.DNN.Modules.FlickrGallery.Models.Photos
{
    public partial class PhotoBase : IHydratable, IPropertyAccess
    {

        #region " IHydratable Methods "

        public virtual void Fill(IDataReader dr)
        {
   PhotoId = Convert.ToInt32(Null.SetNull(dr["PhotoId"], PhotoId));
   FlickrId = Convert.ToString(Null.SetNull(dr["FlickrId"], FlickrId));
   ModuleId = Convert.ToInt32(Null.SetNull(dr["ModuleId"], ModuleId));
   PhotographerId = Convert.ToInt32(Null.SetNull(dr["PhotographerId"], PhotographerId));
   Title = Convert.ToString(Null.SetNull(dr["Title"], Title));
   DateTaken = (DateTime)(Null.SetNull(dr["DateTaken"], DateTaken));
   DateAddedToGroup = (DateTime)(Null.SetNull(dr["DateAddedToGroup"], DateAddedToGroup));
   SquareThumbnailUrl = Convert.ToString(Null.SetNull(dr["SquareThumbnailUrl"], SquareThumbnailUrl));
   Medium800Height = Convert.ToInt32(Null.SetNull(dr["Medium800Height"], Medium800Height));
   Medium800Width = Convert.ToInt32(Null.SetNull(dr["Medium800Width"], Medium800Width));
   Medium800Url = Convert.ToString(Null.SetNull(dr["Medium800Url"], Medium800Url));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return PhotoId; }
            set { PhotoId = value; }
        }
        #endregion

        #region " IPropertyAccess Methods "
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "photoid": // Int
     return PhotoId.ToString(strFormat, formatProvider);
    case "flickrid": // VarChar
     return PropertyAccess.FormatString(FlickrId, strFormat);
    case "moduleid": // Int
     return ModuleId.ToString(strFormat, formatProvider);
    case "photographerid": // Int
     return PhotographerId.ToString(strFormat, formatProvider);
    case "title": // NVarChar
     if (Title == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(Title, strFormat);
    case "datetaken": // DateTime
     return DateTaken.ToString(strFormat, formatProvider);
    case "dateaddedtogroup": // DateTime
     if (DateAddedToGroup == null);
     {
         return "";
     };
     return ((DateTime)DateAddedToGroup).ToString(strFormat, formatProvider);
    case "squarethumbnailurl": // NVarChar
     if (SquareThumbnailUrl == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(SquareThumbnailUrl, strFormat);
    case "medium800height": // Int
     if (Medium800Height == null);
     {
         return "";
     };
     return ((int)Medium800Height).ToString(strFormat, formatProvider);
    case "medium800width": // Int
     if (Medium800Width == null);
     {
         return "";
     };
     return ((int)Medium800Width).ToString(strFormat, formatProvider);
    case "medium800url": // NVarChar
     if (Medium800Url == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(Medium800Url, strFormat);
                default:
                    propertyNotFound = true;
                    break;
            }

            return Null.NullString;
        }

        [IgnoreColumn()]
        public CacheLevel Cacheability
        {
            get { return CacheLevel.fullyCacheable; }
        }
        #endregion

    }
}

