import { LOAD_EMPLOYEES, LOAD_EMPLOYEES_SUCCESS } from "../actions/types";

export default (state = [], action) => {
  switch (action.type) {
    case LOAD_EMPLOYEES:
      return state;
    case LOAD_EMPLOYEES_SUCCESS:
      return action.payload;
    default:
      return state;
  }
};
