import { employeesReducer } from "./employeesReducer";
import { projectsReducer } from "./projectsReducer";
import { customersReducer } from "./customersReducer";
import { combineReducers } from "redux";
import { AnnualReport } from "./annualReportReducer";
import { monthlyReport } from "./monthlyReportReducer"
export default combineReducers({
  employees: employeesReducer,
  projects: projectsReducer,
  customers: customersReducer,
  annualReport: AnnualReport,
  monthlyReport: monthlyReport
})
export { AnnualReport } from "./annualReportReducer";
export { monthlyReport } from "./monthlyReportReducer";
export { userReducer } from "./authReducer";
export { employeesReducer } from "./employeesReducer";
export { customersReducer } from "./customersReducer";
export { projectsReducer } from "./projectsReducer";
