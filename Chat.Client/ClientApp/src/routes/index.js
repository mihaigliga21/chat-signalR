import React from "react";
import { Route, Switch } from "react-router";
import NoMatch from "../components/layout/NoMatch";
import NavMenu from "../components/layout/NavMenu";
import PrivateRoute from "../components/account/PrivateRoute";

import Home from "../components/view/authentificated/Home";

import Login from "../components/view/unauthentificated/Login";

const routes = (
  <React.Fragment>
    <NavMenu />
    <div className="container">
      <Switch>
        <PrivateRoute exact path="/" component={Home} />
        <Route exact path="/login" component={Login} />
        <Route component={NoMatch} />
      </Switch>
    </div>
  </React.Fragment>
);

export default routes;
