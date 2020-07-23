import * as types from "./types";

// CREATE MESSAGE
export const createMessage = (msg) => {
  return {
    type: types.CREATE_MESSAGE,
    payload: msg,
  };
};

// RETURN ERRORS
export const returnErrors = (msg, status) => {
  return {
    type: types.GET_ERRORS,
    payload: { msg, status },
  };
};
