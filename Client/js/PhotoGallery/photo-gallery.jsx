var GalleryPhoto = require("./photo-gallery-photo.jsx");

module.exports = React.createClass({
  getInitialState() {
    return {
      photos: this.props.photos,
      currentPage: 0,
      lastPage: this.props.photos.length < 50,
      loading: false
    };
  },

  onMoreClick(e) {
    e.preventDefault();
    this.loadNextSegment();
  },

  hookUpSwipe() {
    $(".cfg-gallery figure").unbind("click");
    var pswpElement = document.querySelectorAll(".pswp")[0];
    $(".cfg-gallery figure").click(e => {
      var options = {
        index: $(e.currentTarget).index()
      };
      var gallery = new PhotoSwipe(
        pswpElement,
        PhotoSwipeUI_Default,
        this.props.photoList,
        options
      );
      gallery.init();
      e.preventDefault();
    });
    $(".pswp").click(e => {
      e.preventDefault();
    });
  },

  loadNextSegment() {
    if (!this.state.loading) {
      this.setState(
        {
          loading: true
        },
        () => {
          this.props.module.service.nextGallerySegment(
            this.state.currentPage + 1,
            this.props.albumId,
            data => {
              this.setState({
                photos: this.state.photos.concat(data),
                currentPage: this.state.currentPage + 1,
                lastPage: data.length < 50,
                loading: false
              });
            }
          );
        }
      );
    }
  },

  componentDidUpdate() {
    this.hookUpSwipe();
  },

  componentDidMount() {
    $(document).ready(() => {
      $(window).scroll(() => {
        if ($(".scroll-next").length > 0) {
          if (
            window.innerHeight + $(window).scrollTop() >
            $(".scroll-next").offset().top
          ) {
            this.loadNextSegment();
          }
        }
      });
      this.hookUpSwipe();
    });
  },

  render() {
    var photos = this.state.photos.map(item => {
      return <GalleryPhoto photo={item} key={item.PhotoId} />;
    });
    var moreLink = this.state.lastPage ? null : (
      <a href="#" className="scroll-next" onClick={this.onMoreClick}>
        {this.props.module.resources.LoadMore}
      </a>
    );
    return (
      <div
        className="cfg-gallery lstGallery"
        itemscope
        itemtype="http://schema.org/ImageGallery"
      >
        {photos}
        {moreLink}
      </div>
    );
  }
});
