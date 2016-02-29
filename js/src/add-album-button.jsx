module.exports = React.createClass({

  getInitialState() {
    return {
      dialogShown: false
    }
  },

  addClicked(e) {
    e.preventDefault();
    $(this.refs.modal.getDOMNode()).modal('show');
  },

  close() {
  },

  render() {
    var btn = this.props.module.security.CanAdd ? (
       <a href="#" className="dnnSecondaryAction" onClick={this.addClicked}>Add Album</a>
      ) : null;
    return (
      <div>
      {btn}
      
        <div className="modal fade" tabindex="-1" role="dialog" ref="modal">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <button type="button" className="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 className="modal-title">Add Album</h4>
              </div>
              <div className="modal-body">
                <div className="form-group">
                  <label for="txtAlbumName">Album Name</label>
                  <input type="email" className="form-control" ref="txtAlbumName" placeholder="Album Name" />
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-default" data-dismiss="modal">Cancel</button>
                <button type="button" className="btn btn-primary">Create</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      );
  }

});
