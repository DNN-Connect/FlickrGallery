export default class DataService {
    private moduleId: number = -1;
    public baseServicepath = ($ as any).dnnSF(this.moduleId).getServiceRoot('Connect/FlickrGallery');
    constructor(mid: number) {
        this.moduleId = mid;
    };

    private ajaxCall(type: string, controller: string, action: string, id: any, data: any, success?: Function, fail?: Function): void {
        // showLoading();
        $.ajax({
            type: type,
            url: this.baseServicepath + controller + '/' + action + (id != null ? '/' + id : ''),
            beforeSend: ($ as any).dnnSF(this.moduleId).setModuleHeaders,
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

    public nextGallerySegment(pageNr: number, albumId: number, success: Function, fail?: Function) {
        this.ajaxCall('GET', 'Photos', 'Page', pageNr, {albumId: albumId}, success, fail);
    }

    public refresh(success: Function, fail?: Function) {
        this.ajaxCall('POST', 'Synchronization', 'SyncModule', null, null, success, fail);
    }

    public sendFile(fileToSend: any, albumName: string, success: Function, fail?: Function) {
        this.ajaxCall('POST', 'Photos', 'Send', null, {
            fileName: fileToSend.fileName,
            newName: fileToSend.newName,
            albumName: albumName
        }, success, fail);
    }
}
