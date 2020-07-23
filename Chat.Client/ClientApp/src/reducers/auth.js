import * as types from "../actions/types";

const initialState = {
  isAuthenticated: false,
  isLoading: false,
  user: null,
  searchUserList: [],
  searchingForUser: false,
  token: undefined,
  refreshToken: undefined,
};

export default function (state = initialState, action) {
  switch (action.type) {
    case types.AUTHENTICATION:
      return {
        ...state,
        isLoading: true,
      };
    case types.AUTHENTICATION_SUCCESS:
      return {
        ...state,
        isAuthenticated: true,
        isLoading: false,
        user: action.payload.user,
        token: action.payload.token,
        refreshToken: action.payload.refreshToken,
      };
    case types.AUTHENTICATION_ERROR:
      return {
        ...state,
        user: null,
        isAuthenticated: false,
        isLoading: false,
      };
    case types.SEARCH_USER:
      return {
        ...state,
        searchingForUser: true,
      };
    case types.SEARCH_USER_SUCCESS:
      return {
        ...state,
        searchingForUser: false,
        searchUserList: action.payload,
      };
    case types.SEARCH_USER_ERROR:
      return {
        ...state,
        searchingForUser: false,
      };
    default:
      return state;
  }
}
