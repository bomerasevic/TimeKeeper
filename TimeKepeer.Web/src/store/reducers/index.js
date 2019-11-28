import { employeesReducer } from "./employeesReducer";
import { projectsReducer } from "./projectsReducer";
import { customersReducer } from "./customersReducer";
import { combineReducers } from "redux";

export default combineReducers({
  employees: employeesReducer,
  projects: projectsReducer,
  customers: customersReducer,
})

export { userReducer } from "./authReducer";
export { employeesReducer } from "./employeesReducer";
export { customersReducer } from "./customersReducer";
export { projectsReducer } from "./projectsReducer";
