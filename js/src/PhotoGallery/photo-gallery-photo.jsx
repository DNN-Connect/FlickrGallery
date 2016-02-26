module.exports = React.createClass({

  getInitialState() {
    return {
    }
  },

  render() {
    var size = this.props.photo.LargeWidth + 'x' + this.props.photo.LargeHeight;
    return (
       <figure itemprop="associatedMedia" itemscope itemtype="http://schema.org/ImageObject" 
               data-src={this.props.photo.LargeUrl} data-w={this.props.photo.LargeWidth} data-h={this.props.photo.LargeHeight}
               data-title={this.props.photo.Title}>
        <a href={this.props.photo.LargeUrl} itemprop="contentUrl" data-size={size}>
         <img src={this.props.photo.LargeSquareThumbnailUrl} itemprop="thumbnail" alt="Image description" width="150" height="150" />
        </a>
        <figcaption itemprop="caption description">{this.props.photo.Title}</figcaption>
       </figure>
    );
  }

});
