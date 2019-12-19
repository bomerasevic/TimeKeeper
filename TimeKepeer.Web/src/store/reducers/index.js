import { employeesReducer } from "./employeesReducer";
import { projectsReducer } from "./projectsReducer";
import { customersReducer } from "./customersReducer";
import { monthReducer} from "./monthReducer";
import { yearReducer } from "./yearReducer";
import { teamsReducer } from "./teamsReducer";
import { teamTrackingReducer } from "./teamTrackingReducer";
import { combineReducers } from "redux";

export default combineReducers ({
  employees: employeesReducer,
  projects: projectsReducer,
  customers: customersReducer,
  month: monthReducer,
  year: yearReducer,
  teams: teamsReducer,
  teamTracking: teamTrackingReducer
})

export { userReducer } from "./authReducer";
export { employeesReducer } from "./employeesReducer";
export { customersReducer } from "./customersReducer";
export { projectsReducer } from "./projectsReducer";
export { monthReducer } from "./monthReducer";
export { yearReducer } from "./yearReducer";
export { teamsReducer } from "./teamsReducer";
export { teamTrackingReducer } from "./teamTrackingReducer";
