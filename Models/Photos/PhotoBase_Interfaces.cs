
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Photos
{
    public partial class PhotoBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   PhotoId = Convert.ToInt32(Null.SetNull(dr["PhotoId"], PhotoId));
   FlickrId = Convert.ToString(Null.SetNull(dr["FlickrId"], FlickrId));
   ModuleId = Convert.ToInt32(Null.SetNull(dr["ModuleId"], ModuleId));
   PhotographerId = Convert.ToInt32(Null.SetNull(dr["PhotographerId"], PhotographerId));
   Title = Convert.ToString(Null.SetNull(dr["Title"], Title));
   DateTaken = (DateTime)(Null.SetNull(dr["DateTaken"], DateTaken));
   DateAddedToGroup = (DateTime)(Null.SetNull(dr["DateAddedToGroup"], DateAddedToGroup));
   LargeSquareThumbnailUrl = Convert.ToString(Null.SetNull(dr["LargeSquareThumbnailUrl"], LargeSquareThumbnailUrl));
   LargeHeight = Convert.ToInt32(Null.SetNull(dr["LargeHeight"], LargeHeight));
   LargeWidth = Convert.ToInt32(Null.SetNull(dr["LargeWidth"], LargeWidth));
   LargeUrl = Convert.ToString(Null.SetNull(dr["LargeUrl"], LargeUrl));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return PhotoId; }
            set { PhotoId = value; }
        }
        #endregion

        #region IPropertyAccess
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
     if (Title == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Title, strFormat);
    case "datetaken": // DateTime
     return DateTaken.ToString(strFormat, formatProvider);
    case "dateaddedtogroup": // DateTime
     if (DateAddedToGroup == null)
     {
         return "";
     };
     return ((DateTime)DateAddedToGroup).ToString(strFormat, formatProvider);
    case "largesquarethumbnailurl": // NVarChar
     if (LargeSquareThumbnailUrl == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(LargeSquareThumbnailUrl, strFormat);
    case "largeheight": // Int
     if (LargeHeight == null)
     {
         return "";
     };
     return ((int)LargeHeight).ToString(strFormat, formatProvider);
    case "largewidth": // Int
     if (LargeWidth == null)
     {
         return "";
     };
     return ((int)LargeWidth).ToString(strFormat, formatProvider);
    case "largeurl": // NVarChar
     if (LargeUrl == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(LargeUrl, strFormat);
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

