import React, { Component } from "react";
import { connect } from "react-redux";

class NoMatch extends Component {
  render() {
    return (
      <div>
        <h1>404</h1>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {};
};

export default connect(mapStateToProps)(NoMatch);
