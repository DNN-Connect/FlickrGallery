import * as $ from "jquery";
import * as Models from "./Models";
import DataService from "./Service";

export class AppManager {
  public static Modules = new Models.KeyedCollection<Models.IAppModule>();

  public static loadData(): void {
    $(".flickrGallery").each(function(i, el) {
      var moduleId = $(el).data("moduleid");
      AppManager.Modules.Add(
        moduleId,
        new Models.AppModule(
          moduleId,
          $(el).data("tabid"),
          $(el).data("locale"),
          $(el).data("resources"),
          $(el).data("viewtype"),
          $(el).data("security"),
          new DataService(moduleId)
        )
      );
    });
  }
}
