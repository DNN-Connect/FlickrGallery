module.exports = React.createClass({

  getInitialState() {
    return {
    }
  },

  componentWillMount() {
  },
  
  componentWillUnmount() {
  },

  render() {
    var txt = this.props.upload.sending ? this.props.module.resources.sending : '';
    return (
      <div className="cfg-uploaded-file">{this.props.upload.fileName} {txt}</div>
    );
  }

});
