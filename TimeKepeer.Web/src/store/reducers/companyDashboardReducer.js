import {
  FETCH_COMPANY_DASHBOARD,
  FETCH_COMPANY_DASHBOARD_SUCCESS,
  FETCH_COMPANY_DASHBOARD_FAILURE
} from "../actions/types";

const initialState = {
  data: null,
  isLoading: true
};
export const companyDashboardReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_COMPANY_DASHBOARD:
      return Object.assign({}, state, {
        isLoading: true
      });
    // return state;
    case FETCH_COMPANY_DASHBOARD_SUCCESS:
      return Object.assign({}, state, {
        data: action.payload,
        isLoading: false
      });
    case FETCH_COMPANY_DASHBOARD_FAILURE:
      return Object.assign({}, state, {
        error: action.payload
      });
    default:
      return state;
  }
};
