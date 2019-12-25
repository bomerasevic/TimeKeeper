import { employeesReducer } from "./employeesReducer";
import { projectsReducer } from "./projectsReducer";
import { customersReducer } from "./customersReducer";
import { combineReducers } from "redux";
import { AnnualReport } from "./annualReportReducer";
import { MonthlyReport } from "./monthlyReportReducer";
import { monthReducer} from "./monthReducer";
import { yearReducer } from "./yearReducer";
import { teamsReducer } from "./teamsReducer";
import { teamTrackingReducer } from "./teamTrackingReducer";

export default combineReducers({
  employees: employeesReducer,
  projects: projectsReducer,
  customers: customersReducer,
  annualReport: AnnualReport,
  MonthlyReport: MonthlyReport,
  month: monthReducer,
  year: yearReducer,
  teams: teamsReducer,
  teamTracking: teamTrackingReducer

})
export { AnnualReport } from "./annualReportReducer";
export { MonthlyReport } from "./monthlyReportReducer";
export { userReducer } from "./authReducer";
export { employeesReducer } from "./employeesReducer";
export { customersReducer } from "./customersReducer";
export { projectsReducer } from "./projectsReducer";

export { monthReducer } from "./monthReducer";
export { yearReducer } from "./yearReducer";
export { teamsReducer } from "./teamsReducer";
export { teamTrackingReducer } from "./teamTrackingReducer";
