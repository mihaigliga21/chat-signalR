import React, { Component } from "react";
import { BrowserRouter } from "react-router-dom";
import { Provider as AlertProvider } from "react-alert";
import AlertTemplate from "react-alert-template-basic";
import routes from "../routes";
import Alerts from "./layout/Alerts";

import "../style/custom.css";

// Alert Options
const alertOptions = {
  timeout: 3000,
  position: "top center",
  containerStyle: {
    zIndex: 1060,
  },
};

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <AlertProvider template={AlertTemplate} {...alertOptions}>
        <Alerts />
        <BrowserRouter>
          <div className="container-fluid">{routes}</div>
        </BrowserRouter>
      </AlertProvider>
    );
  }
}
