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
"use strict";

module.exports = React.createClass({
  displayName: "exports",
  getInitialState: function getInitialState() {
    return {};
  },
  changed: function changed() {
    var s = this.refs.albumDropdown.getDOMNode();
    var albumName = '';
    if (s.options[s.selectedIndex].value == "-1") {
      albumName = this.refs.newAlbumName.getDOMNode().value;
    } else {
      albumName = s.options[s.selectedIndex].text;
    }
    this.props.setAlbumName(albumName);
  },
  render: function render() {
    var options = this.props.albums.map(function (item) {
      return React.createElement(
        "option",
        { value: item.AlbumId },
        item.Title
      );
    });
    return React.createElement(
      "div",
      null,
      React.createElement(
        "h3",
        null,
        "Album"
      ),
      React.createElement(
        "div",
        null,
        React.createElement(
          "select",
          { className: "form-control", id: "SelectedAlbum",
            ref: "albumDropdown", onChange: this.changed },
          React.createElement(
            "option",
            { value: "-1" },
            "New ..."
          ),
          options
        )
      ),
      React.createElement(
        "table",
        { className: "cfg-table" },
        React.createElement(
          "tr",
          null,
          React.createElement(
            "td",
            { width: "100px;" },
            React.createElement(
              "strong",
              null,
              "New Album"
            )
          ),
          React.createElement(
            "td",
            null,
            React.createElement("input", { type: "text", className: "form-control", id: "NewAlbum",
              ref: "newAlbumName", onChange: this.changed })
          )
        )
      )
    );
  }
});

},{}],4:[function(require,module,exports){
"use strict";

var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

var UploadedFile = require("./uploaded-file.jsx"),
    AlbumSelect = require("./album-select.jsx");

module.exports = React.createClass({
  displayName: "exports",
  getInitialState: function getInitialState() {
    return {
      uploadedFiles: [],
      selectedAlbum: '',
      albums: this.props.albums
    };
  },
  fileUploaded: function fileUploaded(file, response) {
    this.setState({
      uploadedFiles: this.state.uploadedFiles.concat(response)
    });
  },
  submitUploads: function submitUploads(e) {
    e.preventDefault();
    this.sendNextInQueue();
  },
  setAlbumName: function setAlbumName(name) {
    this.setState({
      selectedAlbum: name
    });
  },
  sendNextInQueue: function sendNextInQueue() {
    var _this = this;

    var nextToSend = null;
    var newList = [];
    for (var i = 0; i < this.state.uploadedFiles.length; i++) {
      var up = this.state.uploadedFiles[i];
      if (nextToSend == null & !up.sent) {
        nextToSend = up;
        up.sending = true;
      }
      newList.push(up);
    }
    if (nextToSend != null) {
      this.setState({
        uploadedFiles: newList
      });
      this.props.module.service.sendFile(nextToSend, this.state.selectedAlbum, function (data) {
        nextToSend.sending = false;
        nextToSend.sent = true;
        var newList2 = [];
        for (var i = 0; i < _this.state.uploadedFiles.length; i++) {
          newList2.push(_this.state.uploadedFiles[i].newName == nextToSend.newName ? nextToSend : _this.state.uploadedFiles[i]);
        }
        _this.setState({
          uploadedFiles: newList2
        });
        _this.sendNextInQueue();
      });
    }
  },
  componentDidMount: function componentDidMount() {
    var _this2 = this;

    var that = this;
    $(document).ready(function () {
      Dropzone.autoDiscover = false;
      Dropzone.options.flickrUploadDropzone = {
        init: function init() {
          this.on("success", that.fileUploaded);
        }
      };
      $("#flickrUploadDropzone").dropzone({
        acceptedFiles: '.jpg,.png',
        url: _this2.props.module.service.baseServicepath + 'Photos/SaveUploadedFile',
        headers: {
          moduleId: _this2.props.module.moduleId,
          tabId: _this2.props.module.tabId
        },
        dictDefaultMessage: _this2.props.module.resources.UploadMessage,
        dictFallbackMessage: _this2.props.module.resources.UploadNotSupported,
        parallelUploads: 1
      });
    });
  },
  render: function render() {
    var _this3 = this;

    var uploads = this.state.uploadedFiles.map(function (item) {
      return React.createElement(UploadedFile, _extends({ upload: item, key: item.newName }, _this3.props));
    });
    var unsentUploads = false;
    for (var i = 0; i < this.state.uploadedFiles.length; i++) {
      if (!this.state.uploadedFiles[i].sent) {
        unsentUploads = true;
      };
    }
    var sendButton = unsentUploads ? React.createElement(
      "a",
      { href: "#", className: "dnnPrimaryAction", onClick: this.submitUploads },
      "Submit"
    ) : null;
    var albumSelector = this.props.module.viewType == "User" ? React.createElement(AlbumSelect, _extends({ albums: this.state.albums, setAlbumName: this.setAlbumName }, this.props)) : null;
    return React.createElement(
      "div",
      null,
      React.createElement("div", { ref: "dropzone", id: "flickrUploadDropzone", className: "dropzone" }),
      albumSelector,
      React.createElement(
        "h4",
        null,
        "Upload Files to ",
        this.state.selectedAlbum
      ),
      uploads,
      React.createElement("input", { type: "hidden", id: "UploadedFiles", value: JSON.stringify(this.state.uploadedFiles) }),
      React.createElement(
        "div",
        { className: "cfg-buttons" },
        sendButton
      )
    );
  }
});

},{"./album-select.jsx":3,"./uploaded-file.jsx":5}],5:[function(require,module,exports){
'use strict';

module.exports = React.createClass({
  displayName: 'exports',
  getInitialState: function getInitialState() {
    return {};
  },
  componentWillMount: function componentWillMount() {},
  componentWillUnmount: function componentWillUnmount() {},
  render: function render() {
    var txt = this.props.upload.sending ? 'sending ...' : '';
    return React.createElement(
      'div',
      null,
      this.props.upload.fileName,
      ' ',
      txt
    );
  }
});

},{}],6:[function(require,module,exports){
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

},{}],7:[function(require,module,exports){
"use strict";

var Service = require('./service.js'),
    PhotoGallery = require("./PhotoGallery/photo-gallery.jsx"),
    RefreshButton = require("./refresh-button.jsx"),
    Upload = require("./Upload/upload.jsx");

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
          tabId: $(el).data('tabid'),
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
      $('.flickrUpload').each(function (i, el) {
        React.render(React.createElement(Upload, { module: _this.modules[$(el).data('moduleid')],
          albums: $(el).data('albums') }), el);
      });
    }
  };
})(jQuery, window, document);

},{"./PhotoGallery/photo-gallery.jsx":2,"./Upload/upload.jsx":4,"./refresh-button.jsx":8,"./service.js":9}],8:[function(require,module,exports){
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

},{}],9:[function(require,module,exports){
'use strict';

module.exports = function ($, mid) {
    var moduleId = mid;
    this.baseServicepath = $.dnnSF(moduleId).getServiceRoot('Connect/FlickrGallery');

    this.ajaxCall = function (type, controller, action, id, data, success, fail) {
        // showLoading();
        $.ajax({
            type: type,
            url: this.baseServicepath + controller + '/' + action + (id != null ? '/' + id : ''),
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
    this.sendFile = function (fileToSend, albumName, success, fail) {
        this.ajaxCall('POST', 'Photos', 'Send', null, {
            fileName: fileToSend.fileName,
            newName: fileToSend.newName,
            albumName: albumName
        }, success, fail);
    };
};

},{}]},{},[7,6])

