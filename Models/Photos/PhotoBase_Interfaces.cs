
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
   SquareThumbnailHeight = Convert.ToInt32(Null.SetNull(dr["SquareThumbnailHeight"], SquareThumbnailHeight));
   SquareThumbnailWidth = Convert.ToInt32(Null.SetNull(dr["SquareThumbnailWidth"], SquareThumbnailWidth));
   SquareThumbnailUrl = Convert.ToString(Null.SetNull(dr["SquareThumbnailUrl"], SquareThumbnailUrl));
   ThumbnailHeight = Convert.ToInt32(Null.SetNull(dr["ThumbnailHeight"], ThumbnailHeight));
   ThumbnailWidth = Convert.ToInt32(Null.SetNull(dr["ThumbnailWidth"], ThumbnailWidth));
   ThumbnailUrl = Convert.ToString(Null.SetNull(dr["ThumbnailUrl"], ThumbnailUrl));
   Medium800Height = Convert.ToInt32(Null.SetNull(dr["Medium800Height"], Medium800Height));
   Medium800Width = Convert.ToInt32(Null.SetNull(dr["Medium800Width"], Medium800Width));
   Medium800Url = Convert.ToString(Null.SetNull(dr["Medium800Url"], Medium800Url));
   WebUrl = Convert.ToString(Null.SetNull(dr["WebUrl"], WebUrl));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return PhotoId; }
            set { PhotoId = value; }
        }
        #endregion

        #region " IPropertyAccess Methods "
        public virtual string GetProperty(string strPropertyName, string strFormat, CultureInfo formatProvider, UserInfo accessingUser, Scope accessLevel, ref bool propertyNotFound)
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
    case "squarethumbnailheight": // Int
     if (SquareThumbnailHeight == null);
     {
         return "";
     };
     return ((int)SquareThumbnailHeight).ToString(strFormat, formatProvider);
    case "squarethumbnailwidth": // Int
     if (SquareThumbnailWidth == null);
     {
         return "";
     };
     return ((int)SquareThumbnailWidth).ToString(strFormat, formatProvider);
    case "squarethumbnailurl": // NVarChar
     if (SquareThumbnailUrl == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(SquareThumbnailUrl, strFormat);
    case "thumbnailheight": // Int
     if (ThumbnailHeight == null);
     {
         return "";
     };
     return ((int)ThumbnailHeight).ToString(strFormat, formatProvider);
    case "thumbnailwidth": // Int
     if (ThumbnailWidth == null);
     {
         return "";
     };
     return ((int)ThumbnailWidth).ToString(strFormat, formatProvider);
    case "thumbnailurl": // NVarChar
     if (ThumbnailUrl == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(ThumbnailUrl, strFormat);
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
    case "weburl": // NVarChar
     if (WebUrl == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(WebUrl, strFormat);
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

