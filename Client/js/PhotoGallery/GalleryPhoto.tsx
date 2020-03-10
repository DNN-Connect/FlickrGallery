import * as React from "react";

declare module "react" {
  interface HTMLAttributes<T> extends AriaAttributes, DOMAttributes<T> {
    // extends React's HTMLAttributes
    itemprop?: string;
    itemscope?: any;
    itemtype?: string;
  }
}

interface IGalleryPhotoProps {
  photo: any;
}

const GalleryPhoto: React.SFC<IGalleryPhotoProps> = props => {
  var size = props.photo.LargeWidth + "x" + props.photo.LargeHeight;
  return (
    <figure
      itemprop="associatedMedia"
      itemscope
      itemtype="http://schema.org/ImageObject"
      data-src={props.photo.LargeUrl}
      data-w={props.photo.LargeWidth}
      data-h={props.photo.LargeHeight}
      data-title={props.photo.Title}
    >
      <a href={props.photo.LargeUrl} itemprop="contentUrl" data-size={size}>
        <img
          src={props.photo.LargeSquareThumbnailUrl}
          itemprop="thumbnail"
          alt="Image description"
          width="150"
          height="150"
        />
      </a>
      <figcaption itemprop="caption description">
        {props.photo.Title}
      </figcaption>
    </figure>
  );
};

export default GalleryPhoto;
