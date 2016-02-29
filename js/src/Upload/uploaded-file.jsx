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
    var txt = this.props.upload.sending ? 'sending ...' : '';
    return (
      <div>{this.props.upload.fileName} {txt}</div>
    );
  }

});
