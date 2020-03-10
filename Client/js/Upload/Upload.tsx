import * as React from "react";
import * as Models from "../Models/";
import AlbumSelect from "./AlbumSelect";
import UploadedFile from "./UploadedFile";

interface IUploadProps {
  module: Models.IAppModule;
  albums: string[];
  returnUrl: string;
}

interface IUploadState {
  uploadedFiles: any[];
  selectedAlbum: string;
  albums: string[];
}

declare var Dropzone: any;

export default class Upload extends React.Component<
  IUploadProps,
  IUploadState
> {
  constructor(props: IUploadProps) {
    super(props);
    var album = "";
    if (props.module.viewType == "Group") {
      album = props.module.resources.ThisGroup;
    }
    if (props.module.viewType == "Album") {
      album = props.module.resources.ThisAlbum;
    }
    this.state = {
      uploadedFiles: [],
      selectedAlbum: album,
      albums: props.albums
    };
  }

  fileUploaded(file, response) {
    this.setState({
      uploadedFiles: this.state.uploadedFiles.concat(response)
    });
  }

  submitUploads(e) {
    e.preventDefault();
    this.sendNextInQueue();
  }

  setAlbumName(name) {
    this.setState({
      selectedAlbum: name
    });
  }

  sendNextInQueue() {
    var nextToSend: any = null;
    var newList = [];
    for (var i = 0; i < this.state.uploadedFiles.length; i++) {
      var up = this.state.uploadedFiles[i];
      if ((nextToSend == null) && !up.sent) {
        nextToSend = up;
        up.sending = true;
      }
      newList.push(up);
    }
    if (nextToSend != null) {
      this.setState({
        uploadedFiles: newList
      });
      this.props.module.service.sendFile(
        nextToSend,
        this.state.selectedAlbum,
        data => {
          nextToSend.sending = false;
          nextToSend.sent = true;
          var newList2 = [];
          for (var i = 0; i < this.state.uploadedFiles.length; i++) {
            newList2.push(
              this.state.uploadedFiles[i].newName == nextToSend.newName
                ? nextToSend
                : this.state.uploadedFiles[i]
            );
          }
          this.setState({
            uploadedFiles: newList2
          });
          this.sendNextInQueue();
        }
      );
    }
  }

  componentDidMount() {
    var that = this;
    $(document).ready(() => {
      Dropzone.autoDiscover = false;
      Dropzone.options.flickrUploadDropzone = {
        init: function() {
          this.on("success", that.fileUploaded);
        }
      };
      ($("#flickrUploadDropzone") as any).dropzone({
        acceptedFiles: ".jpg,.png",
        url:
          this.props.module.service.baseServicepath + "Photos/SaveUploadedFile",
        headers: {
          moduleId: this.props.module.moduleId,
          tabId: this.props.module.tabId,
          RequestVerificationToken: $(
            '[name="__RequestVerificationToken"]'
          ).val()
        },
        dictDefaultMessage: this.props.module.resources.UploadMessage,
        dictFallbackMessage: this.props.module.resources.UploadNotSupported,
        parallelUploads: 1
      });
    });
  }

  public render(): JSX.Element {
    var uploads = this.state.uploadedFiles.map(item => {
      return <UploadedFile upload={item} key={item.newName} {...this.props} />;
    });
    var unsentUploads = false;
    for (var i = 0; i < this.state.uploadedFiles.length; i++) {
      if (!this.state.uploadedFiles[i].sent) {
        unsentUploads = true;
      }
    }
    var sendButton = unsentUploads ? (
      <a href="#" className="dnnPrimaryAction" onClick={this.submitUploads}>
        {this.props.module.resources.UploadToFlickr}
      </a>
    ) : null;
    var returnButton =
      unsentUploads || (this.state.uploadedFiles.length == 0) ? null : (
        <a href={this.props.returnUrl} className="dnnPrimaryAction">
          {this.props.module.resources.Return}
        </a>
      );
    var albumSelector =
      this.props.module.viewType == "User" ? (
        <AlbumSelect
          albums={this.state.albums}
          setAlbumName={this.setAlbumName}
          {...this.props}
        />
      ) : null;
    return (
      <div>
        <div
          ref="dropzone"
          id="flickrUploadDropzone"
          className="dropzone"
        ></div>
        {albumSelector}
        <p>
          <strong>
            {this.props.module.resources.UploadTo} {this.state.selectedAlbum}
          </strong>
        </p>
        {uploads}
        <input
          type="hidden"
          id="UploadedFiles"
          value={JSON.stringify(this.state.uploadedFiles)}
        />
        <div className="cfg-buttons">
          <a href={this.props.returnUrl} className="dnnSecondaryAction">
            {this.props.module.resources.Cancel}
          </a>
          {sendButton}
          {returnButton}
        </div>
      </div>
    );
  }
}
