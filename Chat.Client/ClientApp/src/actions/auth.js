import * as types from "./types";
import axios from "axios";
import { returnErrors, createMessage } from "./messages";

export const login = (email, password) => (dispatch) => {
  dispatch({ type: types.AUTHENTICATION });

  // Headers
  const headers = {
    "Content-Type": "application/json",
  };

  // Request Body
  const body = JSON.stringify({ email, password });

  axios({
    url: `${types.apiUrl}/user/auhtentificate`,
    method: "POST",
    headers: headers,
    data: body,
  })
    .then((res) => {
      dispatch({
        type: types.AUTHENTICATION_SUCCESS,
        payload: res.data,
      });
    })
    .catch((err) => {
      console.log(err);
      if (err.response && err.response.status === 400) {
        dispatch(
          createMessage({ messageError: err.response.data.Errors[0].Message })
        );
      } else {
        dispatch(returnErrors({ message: "error", status: 500 }));
      }
      dispatch({
        type: types.AUTHENTICATION_ERROR,
      });
    });
};

export const searchUsers = (email) => (dispatch, getState) => {
  dispatch({ type: types.SEARCH_USER });

  const token = getState().auth.token;

  // Headers
  const headers = {
    "Content-Type": "application/json",
    Authorization: `Bearer ${token}`,
  };

  // Request Body
  const body = JSON.stringify({ email });

  axios({
    url: `${types.apiUrl}/user/search-user`,
    method: "POST",
    headers: headers,
    data: body,
  })
    .then((res) => {
      dispatch({
        type: types.SEARCH_USER_SUCCESS,
        payload: res.data,
      });
    })
    .catch((err) => {
      console.log(err);
      if (err.response && err.response.status === 400) {
        dispatch(
          createMessage({ messageError: err.response.data.Errors[0].Message })
        );
      } else {
        dispatch(returnErrors({ message: "error", status: 500 }));
      }
      dispatch({
        type: types.SEARCH_USER_ERROR,
      });
    });
};
