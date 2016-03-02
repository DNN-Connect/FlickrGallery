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
       <a href="#" className="dnnSecondaryAction" onClick={this.refresh}>{this.props.module.resources.Refresh}</a>
      ) : null;
    return btn;
  }

});
