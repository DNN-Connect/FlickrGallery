import * as React from "react";
import * as Models from "../Models/";

interface IUploadedFileProps {
  module: Models.IAppModule;
  upload: any;
}

const UploadedFile: React.SFC<IUploadedFileProps> = props => {
  var txt = props.upload.sending ? props.module.resources.sending : "";
  return (
    <div className="cfg-uploaded-file">
      {props.upload.fileName} {txt}
    </div>
  );
};

export default UploadedFile;
