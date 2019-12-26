import {
  FETCH_ANNUAL_REPORT,
  FETCH_ANNUAL_REPORT_SUCCESS,
  FETCH_ANNUAL_REPORT_FAILURE
} from "../actions/types";

const initialState = {
  table: {
    head: [],
    rows: [],
    actions: false
  },
  error: null,
  isLoading: true
};
export const AnnualReport = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_ANNUAL_REPORT:
      return Object.assign({}, state, {
        isLoading: true
      });
    // return state;
    case FETCH_ANNUAL_REPORT_SUCCESS:
      return Object.assign({}, state, {
        table: {
          head: Object.keys(action.payload[0]),
          rows: action.payload,
          actions: false
        },
        isLoading: false
      });
    case FETCH_ANNUAL_REPORT_FAILURE:
      return Object.assign({}, state, {
        error: action.payload
      });
    default:
      return state;
  }
};