var galleryService;

function GalleryService($, settings, mid) {
 var moduleId = mid;
 var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Connect/FlickrGallery');

 this.ServicePath = baseServicepath;

 this.viewCall = function (controller, action, data, id, success, fail) {
  showLoading();
  var path = baseServicepath + controller + '/' + action;
  if (id != null) { path += '/' + id }
  $.ajax({
   type: "GET",
   url: path,
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: data
  }).done(function (data) {
   hideLoading();
   if (success != undefined) {
    success(data);
   }
  }).fail(function (xhr, status) {
   showError(xhr.responseText);
   if (fail != undefined) {
    fail(xhr.responseText);
   }
  });
 }

 this.nextGallerySegment = function (id, view, success, fail) {
  this.viewCall('Photos', 'Page', { view: view }, id, success, fail);
 }
 this.list = function (success, fail) {
  this.viewCall('Photos', 'List', null, null, success, fail);
 }
 this.album = function (albumId, success, fail) {
  this.viewCall('Photos', 'Album', { album: albumId }, null, success, fail);
 }
}

function showLoading() {
 if ($('#cgStatus').length) {
  $('#cgStatus div:first-child').show();
  $('#cgStatus div:nth-child(2)').hide();
  $('#cgStatus').css('background', '#2FC1F3').show();
 }
}

function hideLoading() {
 if ($('#cgStatus').length) {
  $('#cgStatus').hide();
 }
}

function showError(message) {
 if ($('#cgStatus').length) {
  $('#cgStatus div:first-child').hide();
  $('#cgStatus div:nth-child(2)').html(message).show();
  $('#cgStatus').css('background', '#F33B2F').show();
  setTimeout(function () { $('#cgStatus').hide(); }, 3000);
 }
}

var allSlides = [];
var initSwipe = function (slideSelector) {
 $(slideSelector).unbind("click");
 var pswpElement = document.querySelectorAll('.pswp')[0];
 $(slideSelector).click(function () {
  var options = {
   index: $(this).index()
  };
  var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, allSlides, options);
  gallery.init();
  return false;
 });
}

