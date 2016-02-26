var React = require("react"),
    Service = require('./service.js'),
    PhotoGallery = require("./PhotoGallery/photo-gallery.jsx"),
    RefreshButton = require("./refresh-button.jsx");

(function($, window, document, undefined) {

  $(document).ready(() => {
    ConnectFlickrGallery.loadData();
  });

  window.ConnectFlickrGallery = {

    modules: {},

    loadData() {
      $('.flickrGallery').each((i, el) => {
        this.modules[$(el).data('moduleid')] = {
          moduleId: $(el).data('moduleid'),
          resources: $(el).data('resources'),
          security: $(el).data('security'),
          viewType: $(el).data('viewtype'),
          service: new Service($, $(el).data('moduleid'))
        };
      });
      $('.photoGallery').each((i, el) => {
        React.render(<PhotoGallery module={this.modules[$(el).data('moduleid')]} 
                                   photos={$(el).data('photos')}
                                   albumId={$(el).data('albumid')}
                                   photoList={$(el).data('list')} />, el);
      });
      $('.refreshButton').each((i, el) => {
        React.render(<RefreshButton module={this.modules[$(el).data('moduleid')]} />, el);
      });
    }

  }

})(jQuery, window, document);

