module.exports = function($, mid) {
    var moduleId = mid;
    var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Connect/FlickrGallery');

    this.ajaxCall = function(type, controller, action, id, data, success, fail) {
        // showLoading();
        $.ajax({
            type: type,
            url: baseServicepath + controller + '/' + action + (id != null ? '/' + id : ''),
            beforeSend: $.dnnSF(moduleId).setModuleHeaders,
            data: data
        }).done(function(retdata) {
            // hideLoading();
            if (success != undefined) {
                success(retdata);
            }
        }).fail(function(xhr, status) {
            // showError(xhr.responseText);
            if (fail != undefined) {
                fail(xhr.responseText);
            }
        });
    }

    this.nextGallerySegment = function(pageNr, albumId, success, fail) {
        this.ajaxCall('GET', 'Photos', 'Page', pageNr, {albumId: albumId}, success, fail);
    }
    this.refresh = function(success, fail) {
        this.ajaxCall('POST', 'Synchronization', 'SyncModule', null, null, success, fail);
    }

}
