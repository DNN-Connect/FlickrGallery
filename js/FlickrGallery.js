(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
"use strict";

module.exports = React.createClass({
  displayName: "exports",
  getInitialState: function getInitialState() {
    return {};
  },
  render: function render() {
    var size = this.props.photo.LargeWidth + 'x' + this.props.photo.LargeHeight;
    return React.createElement(
      "figure",
      { itemprop: "associatedMedia", itemscope: true, itemtype: "http://schema.org/ImageObject",
        "data-src": this.props.photo.LargeUrl, "data-w": this.props.photo.LargeWidth, "data-h": this.props.photo.LargeHeight,
        "data-title": this.props.photo.Title },
      React.createElement(
        "a",
        { href: this.props.photo.LargeUrl, itemprop: "contentUrl", "data-size": size },
        React.createElement("img", { src: this.props.photo.LargeSquareThumbnailUrl, itemprop: "thumbnail", alt: "Image description", width: "150", height: "150" })
      ),
      React.createElement(
        "figcaption",
        { itemprop: "caption description" },
        this.props.photo.Title
      )
    );
  }
});

},{}],2:[function(require,module,exports){
"use strict";

var GalleryPhoto = require("./photo-gallery-photo.jsx");

module.exports = React.createClass({
  displayName: "exports",
  getInitialState: function getInitialState() {
    return {
      photos: this.props.photos,
      currentPage: 0,
      lastPage: this.props.photos.length < 50
    };
  },
  onMoreClick: function onMoreClick(e) {
    e.preventDefault();
    this.loadNextSegment();
  },
  hookUpSwipe: function hookUpSwipe() {
    var _this = this;

    $('.cfg-gallery figure').unbind("click");
    var pswpElement = document.querySelectorAll('.pswp')[0];
    $('.cfg-gallery figure').click(function (e) {
      var options = {
        index: $(e.currentTarget).index()
      };
      var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, _this.props.photoList, options);
      gallery.init();
      e.preventDefault();
    });
    $('.pswp').click(function (e) {
      e.preventDefault();
    });
  },
  loadNextSegment: function loadNextSegment() {
    var _this2 = this;

    this.props.module.service.nextGallerySegment(this.state.currentPage + 1, this.props.albumId, function (data) {
      _this2.setState({
        photos: _this2.state.photos.concat(data),
        currentPage: _this2.state.currentPage + 1,
        lastPage: data.length < 50
      });
    });
  },
  componentDidUpdate: function componentDidUpdate() {
    this.hookUpSwipe();
  },
  componentDidMount: function componentDidMount() {
    var _this3 = this;

    $(document).ready(function () {
      $(window).scroll(function () {
        if ($('.scroll-next').length > 0) {
          if (window.innerHeight + $(window).scrollTop() > $('.scroll-next').offset().top) {
            _this3.loadNextSegment();
          }
        }
      });
      _this3.hookUpSwipe();
    });
  },
  render: function render() {
    var photos = this.state.photos.map(function (item) {
      return React.createElement(GalleryPhoto, { photo: item, key: item.PhotoId });
    });
    var moreLink = this.state.lastPage ? null : React.createElement(
      "a",
      { href: "#", className: "scroll-next", onClick: this.onMoreClick },
      this.props.module.resources.LoadMore
    );
    return React.createElement(
      "div",
      { className: "cfg-gallery lstGallery", itemscope: true, itemtype: "http://schema.org/ImageGallery" },
      photos,
      moreLink
    );
  }
});

},{"./photo-gallery-photo.jsx":1}],3:[function(require,module,exports){
'use strict';

function isInt(value) {
  return !isNaN(value) && parseInt(Number(value)) == value && !isNaN(parseInt(value, 10));
}

function validateForm(form, submitButton, formErrorDiv) {
  submitButton.click(function () {
    var hasErrors = false;
    formErrorDiv.empty().hide();
    form.find('input[data-validator="integer"]').each(function (i, el) {
      if (!isInt($(el).val()) & $(el).val() != '') {
        hasErrors = true;
        $(el).parent().addClass('error');
        formErrorDiv.append('<span>' + $(el).attr('data-message') + '</span><br />').show();
      }
    });
    form.find('input[data-required="true"]').each(function (i, el) {
      if ($(el).val() == '') {
        hasErrors = true;
        $(el).parent().addClass('error');
        formErrorDiv.append('<span>' + $(el).attr('data-message') + '</span><br />').show();
      }
    });
    return !hasErrors;
  });
}

function getTableOrder(tableId) {
  var res = [];
  $('#' + tableId + ' tbody:first tr').each(function (i, el) {
    res.push({
      id: $(el).data('id'),
      order: i
    });
  });
  return res;
}

function minutesToTime(mins) {
  var hr = Math.floor(mins / 60);
  var mn = mins - 60 * hr;
  var res = mn.toString();
  if (res.length == 1) {
    res = "0" + res;
  }
  res = hr.toString() + ":" + res;
  return res;
}

if (!String.prototype.startsWith) {
  String.prototype.startsWith = function (searchString, position) {
    position = position || 0;
    return this.indexOf(searchString, position) === position;
  };
}

function pad(number) {
  if (number < 10) {
    return '0' + number;
  }
  return number;
}

},{}],4:[function(require,module,exports){
"use strict";

var Service = require('./service.js'),
    PhotoGallery = require("./PhotoGallery/photo-gallery.jsx"),
    RefreshButton = require("./refresh-button.jsx");

(function ($, window, document, undefined) {

  $(document).ready(function () {
    ConnectFlickrGallery.loadData();
  });

  window.ConnectFlickrGallery = {

    modules: {},

    loadData: function loadData() {
      var _this = this;

      $('.flickrGallery').each(function (i, el) {
        _this.modules[$(el).data('moduleid')] = {
          moduleId: $(el).data('moduleid'),
          resources: $(el).data('resources'),
          security: $(el).data('security'),
          viewType: $(el).data('viewtype'),
          service: new Service($, $(el).data('moduleid'))
        };
      });
      $('.photoGallery').each(function (i, el) {
        React.render(React.createElement(PhotoGallery, { module: _this.modules[$(el).data('moduleid')],
          photos: $(el).data('photos'),
          albumId: $(el).data('albumid'),
          photoList: $(el).data('list') }), el);
      });
      $('.refreshButton').each(function (i, el) {
        React.render(React.createElement(RefreshButton, { module: _this.modules[$(el).data('moduleid')] }), el);
      });
    }
  };
})(jQuery, window, document);

},{"./PhotoGallery/photo-gallery.jsx":2,"./refresh-button.jsx":5,"./service.js":6}],5:[function(require,module,exports){
"use strict";

module.exports = React.createClass({
  displayName: "exports",
  getInitialState: function getInitialState() {
    return {};
  },
  refresh: function refresh() {
    this.props.module.service.refresh(function () {});
  },
  render: function render() {
    var btn = this.props.module.security.IsAdmin ? React.createElement(
      "div",
      { className: "cfg-buttons" },
      React.createElement(
        "a",
        { href: "#", className: "dnnSecondaryAction", onClick: this.refresh },
        "Refresh"
      )
    ) : null;
    return btn;
  }
});

},{}],6:[function(require,module,exports){
'use strict';

module.exports = function ($, mid) {
    var moduleId = mid;
    var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Connect/FlickrGallery');

    this.ajaxCall = function (type, controller, action, id, data, success, fail) {
        // showLoading();
        $.ajax({
            type: type,
            url: baseServicepath + controller + '/' + action + (id != null ? '/' + id : ''),
            beforeSend: $.dnnSF(moduleId).setModuleHeaders,
            data: data
        }).done(function (retdata) {
            // hideLoading();
            if (success != undefined) {
                success(retdata);
            }
        }).fail(function (xhr, status) {
            // showError(xhr.responseText);
            if (fail != undefined) {
                fail(xhr.responseText);
            }
        });
    };

    this.nextGallerySegment = function (pageNr, albumId, success, fail) {
        this.ajaxCall('GET', 'Photos', 'Page', pageNr, { albumId: albumId }, success, fail);
    };
    this.refresh = function (success, fail) {
        this.ajaxCall('POST', 'Synchronization', 'SyncModule', null, null, success, fail);
    };
};

},{}]},{},[4,3])

