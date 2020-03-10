import * as React from "react";
import * as Models from "../Models/";

interface IAlbumSelectProps {
  module: Models.IAppModule;
  albums: any[];
  setAlbumName: any;
}

interface IAlbumSelectState {}

export default class AlbumSelect extends React.Component<
  IAlbumSelectProps,
  IAlbumSelectState
> {
  refs: {
    albumDropdown: HTMLSelectElement;
    newAlbumName: HTMLInputElement;
  };

  constructor(props: IAlbumSelectProps) {
    super(props);
    this.state = {};
  }

  changed() {
    var s = this.refs.albumDropdown;
    var albumName = "";
    if (s.options[s.selectedIndex].value == "-1") {
      albumName = this.refs.newAlbumName.value;
    } else {
      albumName = s.options[s.selectedIndex].text;
    }
    this.props.setAlbumName(albumName);
  }

  public render(): JSX.Element {
    var options = this.props.albums.map(item => {
      return <option value={item.AlbumId}>{item.Title}</option>;
    });
    return (
      <div className="cfg-album-selector">
        <p>
          <strong>{this.props.module.resources.Album}</strong>
        </p>
        <div>
          <select
            className="form-control"
            id="SelectedAlbum"
            ref="albumDropdown"
            onChange={this.changed}
          >
            <option value="-1">{this.props.module.resources.New}</option>
            {options}
          </select>
        </div>
        <table className="cfg-table">
          <tr>
            <td style={{ width: "100px" }}>
              <strong>{this.props.module.resources.NewAlbum}</strong>
            </td>
            <td>
              <input
                type="text"
                className="form-control"
                id="NewAlbum"
                ref="newAlbumName"
                onChange={this.changed}
              />
            </td>
          </tr>
        </table>
      </div>
    );
  }
}
