import {
  FETCH_TEAM_DASHBOARD,
  FETCH_TEAM_DASHBOARD_SUCCESS,
  FETCH_TEAM_DASHBOARD_FAILURE
} from "../actions/types";

const initialState = {
  data: null,
  isLoading: true
};
export const teamDashboardReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_TEAM_DASHBOARD:
      return Object.assign({}, state, {
        isLoading: true
      });
    // return state;
    case FETCH_TEAM_DASHBOARD_SUCCESS:
      // console.log("teamdashboard", action.payload);
      return Object.assign({}, state, {
        data: action.payload,
        isLoading: false
      });
    case FETCH_TEAM_DASHBOARD_FAILURE:
      return Object.assign({}, state, {
        error: action.payload
      });
    default:
      return state;
  }
};
