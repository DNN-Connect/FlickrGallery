
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.AlbumPhotos
{
    public partial class AlbumPhotoBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   AlbumId = Convert.ToInt32(Null.SetNull(dr["AlbumId"], AlbumId));
   PhotoId = Convert.ToInt32(Null.SetNull(dr["PhotoId"], PhotoId));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return Null.NullInteger; }
            set { }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "albumid": // Int
     return AlbumId.ToString(strFormat, formatProvider);
    case "photoid": // Int
     return PhotoId.ToString(strFormat, formatProvider);
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

