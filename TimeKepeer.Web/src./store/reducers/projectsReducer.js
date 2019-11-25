import { LOAD_PROJECTS, LOAD_PROJECTS_SUCCESS } from "../actions/types";

export default (state = [], action) => {
  switch (action.type) {
    case LOAD_PROJECTS:
      return state;
    case LOAD_PROJECTS_SUCCESS:
      return action.payload;
    default:
      return state;
  }
};
