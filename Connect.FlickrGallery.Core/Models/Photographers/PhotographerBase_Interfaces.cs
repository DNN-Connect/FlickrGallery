using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Photographers
{
    public partial class PhotographerBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
            PhotographerId = Convert.ToInt32(Null.SetNull(dr["PhotographerId"], PhotographerId));
            ModuleId = Convert.ToInt32(Null.SetNull(dr["ModuleId"], ModuleId));
            FlickrId = Convert.ToString(Null.SetNull(dr["FlickrId"], FlickrId));
            OwnerName = Convert.ToString(Null.SetNull(dr["OwnerName"], OwnerName));
            UserId = Convert.ToInt32(Null.SetNull(dr["UserId"], UserId));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return PhotographerId; }
            set { PhotographerId = value; }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
                case "photographerid": // Int
                    return PhotographerId.ToString(strFormat, formatProvider);
                case "moduleid": // Int
                    return ModuleId.ToString(strFormat, formatProvider);
                case "flickrid": // NVarChar
                    return PropertyAccess.FormatString(FlickrId, strFormat);
                case "ownername": // NVarChar
                    return PropertyAccess.FormatString(OwnerName, strFormat);
                case "userid": // Int
                    if (UserId == null)
                    {
                        return "";
                    };
                    return ((int)UserId).ToString(strFormat, formatProvider);
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

