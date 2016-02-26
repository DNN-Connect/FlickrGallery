module.exports = React.createClass({

  getInitialState() {
    return {
    }
  },

  refresh() {
    this.props.module.service.refresh(() => {
    });
  },

  render() {
    var btn = this.props.module.security.IsAdmin ? (
      <div className="cfg-buttons">
       <a href="#" className="dnnSecondaryAction" onClick={this.refresh}>Refresh</a>
      </div>
      ) : null;
    return btn;
  }

});
