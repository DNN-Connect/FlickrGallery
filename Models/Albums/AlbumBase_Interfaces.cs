
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Albums
{
    public partial class AlbumBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   AlbumId = Convert.ToInt32(Null.SetNull(dr["AlbumId"], AlbumId));
   ModuleId = Convert.ToInt32(Null.SetNull(dr["ModuleId"], ModuleId));
   PhotosetId = Convert.ToString(Null.SetNull(dr["PhotosetId"], PhotosetId));
   Title = Convert.ToString(Null.SetNull(dr["Title"], Title));
   PrimaryPhotoId = Convert.ToInt32(Null.SetNull(dr["PrimaryPhotoId"], PrimaryPhotoId));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return AlbumId; }
            set { AlbumId = value; }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "albumid": // Int
     return AlbumId.ToString(strFormat, formatProvider);
    case "moduleid": // Int
     return ModuleId.ToString(strFormat, formatProvider);
    case "photosetid": // VarChar
     return PropertyAccess.FormatString(PhotosetId, strFormat);
    case "title": // NVarChar
     if (Title == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Title, strFormat);
    case "primaryphotoid": // Int
     if (PrimaryPhotoId == null)
     {
         return "";
     };
     return ((int)PrimaryPhotoId).ToString(strFormat, formatProvider);
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

