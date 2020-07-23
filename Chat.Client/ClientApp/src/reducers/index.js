import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";

import auth from "./auth";
import messages from "./messages";
import errors from "./errors";

const createRootReducer = (history) =>
  combineReducers({
    router: connectRouter(history),
    auth,
    messages,
    errors,
  });
export default createRootReducer;
