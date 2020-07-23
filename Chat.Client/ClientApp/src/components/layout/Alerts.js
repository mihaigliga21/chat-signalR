import React, { Component, Fragment } from "react";
import { withAlert } from "react-alert";
import { connect } from "react-redux";
import PropTypes from "prop-types";

export class Alerts extends Component {
  static propTypes = {
    error: PropTypes.object.isRequired,
    message: PropTypes.object.isRequired,
  };

  componentDidUpdate(prevProps) {
    const { error, alert, message } = this.props;
    console.log(error);
    console.log(message);
    if (error !== prevProps.error) {
      if (error.msg.message) alert.error(error.msg.message);
    }

    if (message !== prevProps.message) {
      //error
      if (message.messageError) alert.error(message.messageError);
      //success
      if (message.messageSuccess) alert.success(message.messageSuccess);
    }
  }

  render() {
    return <Fragment />;
  }
}

const mapStateToProps = (state) => ({
  error: state.errors,
  message: state.messages,
});

export default connect(mapStateToProps)(withAlert()(Alerts));
