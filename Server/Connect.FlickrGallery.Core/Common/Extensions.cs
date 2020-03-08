using Connect.FlickrGallery.Core.Models.AlbumPhotos;
using Connect.FlickrGallery.Core.Models.Photos;
using System.Collections.Generic;

namespace Connect.FlickrGallery.Core.Common
{
    public static class Extensions
    {
        public static List<PhotoSwipePhoto> ToPhotoSwipeList(this IEnumerable<Photo> input)
        {
            List<PhotoSwipePhoto> res = new List<PhotoSwipePhoto>();
            foreach (var p in input)
            {
                res.Add(new PhotoSwipePhoto() { src = p.LargeUrl, w = p.LargeWidth, h = p.LargeHeight, title = p.Title });
            }
            return res;   
        }
        public static List<PhotoSwipePhoto> ToPhotoSwipeList(this IEnumerable<AlbumPhoto> input)
        {
            List<PhotoSwipePhoto> res = new List<PhotoSwipePhoto>();
            foreach (var p in input)
            {
                res.Add(new PhotoSwipePhoto() { src = p.LargeUrl, w = p.LargeWidth, h = p.LargeHeight, title = p.Title });
            }
            return res;
        }
    }
}
