using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Connect.FlickrGallery.Core.Models.Albums
{

    [Serializable(), XmlRoot("Album")]
    public partial class Album
    {

        #region IHydratable
        public override void Fill(IDataReader dr)
        {
            base.Fill(dr);
            LargeHeight = Convert.ToInt32(Null.SetNull(dr["LargeHeight"], LargeHeight));
            LargeSquareThumbnailUrl = Convert.ToString(Null.SetNull(dr["LargeSquareThumbnailUrl"], LargeSquareThumbnailUrl));
            LargeUrl = Convert.ToString(Null.SetNull(dr["LargeUrl"], LargeUrl));
            LargeWidth = Convert.ToInt32(Null.SetNull(dr["LargeWidth"], LargeWidth));
            NrPhotos = Convert.ToInt32(Null.SetNull(dr["NrPhotos"], NrPhotos));
        }
        #endregion

        #region IPropertyAccess
        public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
                case "largeheight": // Int
                    if (LargeHeight == null)
                    {
                        return "";
                    };
                    return ((int)LargeHeight).ToString(strFormat, formatProvider);
                case "largesquarethumbnailurl": // NVarChar
                    if (LargeSquareThumbnailUrl == null)
                    {
                        return "";
                    };
                    return PropertyAccess.FormatString(LargeSquareThumbnailUrl, strFormat);
                case "largeurl": // NVarChar
                    if (LargeUrl == null)
                    {
                        return "";
                    };
                    return PropertyAccess.FormatString(LargeUrl, strFormat);
                case "largewidth": // Int
                    if (LargeWidth == null)
                    {
                        return "";
                    };
                    return ((int)LargeWidth).ToString(strFormat, formatProvider);
                case "nrphotos": // Int
                    if (NrPhotos == null)
                    {
                        return "";
                    };
                    return ((int)NrPhotos).ToString(strFormat, formatProvider);
                default:
                    return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
            }
        }
        #endregion

    }
}

