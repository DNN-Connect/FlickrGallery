module.exports = React.createClass({

  getInitialState() {
    return {
      refreshing: false
    }
  },

  refresh(e) {
    e.preventDefault();
    if (this.state.refreshing) { return; }
    if (confirm(this.props.module.resources.RefreshConfirm)) {
      this.setState({
        refreshing: true
      });
      this.props.module.service.refresh(() => {
        this.setState({
          refreshing: false
        });
      });
    }
  },

  render() {
    var btn = null;
    if (this.props.module.security.IsAdmin) {
      if (this.state.refreshing) {
        btn = <span className="dnnSecondaryAction disabled">{this.props.module.resources.Refreshing}</span>
      } else {
        btn = <a href="#" className="dnnSecondaryAction" onClick={this.refresh}>{this.props.module.resources.Refresh}</a>;
      }
    }
    return btn;
  }

});
