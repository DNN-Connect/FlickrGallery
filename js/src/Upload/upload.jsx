var UploadedFile = require("./uploaded-file.jsx"),
    AlbumSelect = require("./album-select.jsx");

module.exports = React.createClass({

  getInitialState() {
    return {
      uploadedFiles: [],
      selectedAlbum: '',
      albums: this.props.albums
    }
  },

  fileUploaded(file, response) {
    this.setState({
      uploadedFiles: this.state.uploadedFiles.concat(response)
    });
  },

  submitUploads(e) {
    e.preventDefault();
    this.sendNextInQueue();
  },

  setAlbumName(name) {
    this.setState({
      selectedAlbum: name
    });
  },

  sendNextInQueue() {
    var nextToSend = null;
    var newList = [];
    for (var i=0;i<this.state.uploadedFiles.length;i++) {
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
      this.props.module.service.sendFile(nextToSend, this.state.selectedAlbum, (data) => {
        nextToSend.sending = false;
        nextToSend.sent = true;
        var newList2 = [];
        for (var i=0;i<this.state.uploadedFiles.length;i++) {
          newList2.push((this.state.uploadedFiles[i].newName == nextToSend.newName) ? nextToSend : this.state.uploadedFiles[i]);
        }
        this.setState({
          uploadedFiles: newList2
        });
        this.sendNextInQueue();
      })
    }
  },

  componentDidMount() {
    var that = this;
    $(document).ready(() => {
      Dropzone.autoDiscover = false;
      Dropzone.options.flickrUploadDropzone = {
        init: function() {
          this.on("success", that.fileUploaded);
        }
      };
      $("#flickrUploadDropzone").dropzone({
        acceptedFiles: '.jpg,.png',
        url: this.props.module.service.baseServicepath + 'Photos/SaveUploadedFile',
        headers: { 
          moduleId: this.props.module.moduleId,
          tabId: this.props.module.tabId
        },
        dictDefaultMessage: this.props.module.resources.UploadMessage,
        dictFallbackMessage: this.props.module.resources.UploadNotSupported,
        parallelUploads: 1
      });
    });
  },

  render() {
    var uploads = this.state.uploadedFiles.map((item) => {
      return <UploadedFile upload={item} key={item.newName} {...this.props} />
    });
    var unsentUploads = false;
    for (var i=0;i<this.state.uploadedFiles.length;i++)
    {
      if (!this.state.uploadedFiles[i].sent) { unsentUploads=true };
    }
    var sendButton = unsentUploads ? (
      <a href="#" className="dnnPrimaryAction" onClick={this.submitUploads}>Submit</a>
      ) : null;
    var albumSelector = this.props.module.viewType == "User" ? 
                      <AlbumSelect albums={this.state.albums} setAlbumName={this.setAlbumName} {...this.props} /> : 
                      null;
    return (
      <div>
       <div ref="dropzone" id="flickrUploadDropzone" className="dropzone">
       </div>
       {albumSelector}
       <h4>Upload Files to {this.state.selectedAlbum}</h4>
       {uploads}
       <input type="hidden" id="UploadedFiles" value={JSON.stringify(this.state.uploadedFiles)} />
       <div className="cfg-buttons">
         {sendButton}
       </div>
      </div>
    );
  }

});
