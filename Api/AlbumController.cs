using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DotNetNuke.Web.Api;
using Connect.FlickrGallery.Core.Repositories;

namespace Connect.DNN.Modules.FlickrGallery.Api
{

    public partial class AlbumController : FlickrGalleryApiController
    {
        private readonly IAlbumRepository _repository;

        public AlbumController() : this(AlbumRepository.Instance) { }

        public AlbumController(IAlbumRepository repository)
        {
            Requires.NotNull(repository);
            _repository = repository;
        }

    }
}
