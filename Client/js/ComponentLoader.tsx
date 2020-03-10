import * as React from "react";
import * as ReactDOM from "react-dom";
import * as $ from "jquery";
import PhotoGallery from "./PhotoGallery/PhotoGallery";
import RefreshButton from "./Refresh/RefreshButton";
import Upload from "./Upload/Upload";

import { AppManager } from "./AppManager";

export class ComponentLoader {
  public static load(): void {
    $(".photoGallery").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      ReactDOM.render(
        <PhotoGallery
          module={AppManager.Modules.Item(moduleId.toString())}
          photos={$(el).data("photos")}
          albumId={$(el).data("albumid")}
          photoList={$(el).data("list")}
        />,
        el
      );
    });
    $(".refreshButton").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      ReactDOM.render(
        <RefreshButton module={AppManager.Modules.Item(moduleId.toString())} />,
        el
      );
    });
    $(".flickrUpload").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      ReactDOM.render(
        <Upload
          module={AppManager.Modules.Item(moduleId.toString())}
          albums={$(el).data("albums")}
          returnUrl={$(el).data("returnurl")}
        />,
        el
      );
    });
  }
}
