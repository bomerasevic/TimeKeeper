import { LOAD_CUSTOMERS, LOAD_CUSTOMERS_SUCCESS } from "../actions/types";

export default (state = [], action) => {
  switch (action.type) {
    case LOAD_CUSTOMERS:
      return state;
    case LOAD_CUSTOMERS_SUCCESS:
      return action.payload;
    default:
      return state;
  }
};
