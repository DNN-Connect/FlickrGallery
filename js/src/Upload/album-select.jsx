module.exports = React.createClass({

  getInitialState() {
    return {
    }
  },

  changed() {
    var s = this.refs.albumDropdown.getDOMNode();
    var albumName = '';
    if (s.options[s.selectedIndex].value == "-1") {
      albumName = this.refs.newAlbumName.getDOMNode().value;
    } else {
      albumName = s.options[s.selectedIndex].text;
    }
    this.props.setAlbumName(albumName);
  },

  render() {
    var options = this.props.albums.map((item) => {
      return <option value={item.AlbumId}>{item.Title}</option>
    });
    return (
      <div>
       <h3>Album</h3>
       <div>
       <select className="form-control" id="SelectedAlbum" 
               ref="albumDropdown" onChange={this.changed}>
         <option value="-1">New ...</option>
         {options}
       </select>
       </div>
       <table className="cfg-table">
       <tr>
        <td width="100px;">
         <strong>New Album</strong>
        </td>
        <td>
          <input type="text" className="form-control" id="NewAlbum" 
                 ref="newAlbumName" onChange={this.changed} />
        </td>
        </tr>
       </table>
      </div>
    );
  }

});
