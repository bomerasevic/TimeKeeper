import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
//import { reducer as oidcReducer } from "redux-oidc";
//import createOidcMiddleware from "redux-oidc";
import thunk from "redux-thunk";

//import userManager from "../utils/userManager";

//const oidcMiddleware = createOidcMiddleware(userManager);

import { employeesReducer, customersReducer, projectsReducer, userReducer, AnnualReport, monthlyReport } from "./reducers/index";
const rootReducer = combineReducers({
	employees: employeesReducer,
	user: userReducer,
	customers: customersReducer,
	projects: projectsReducer,
	annualReport: AnnualReport,
	monthlyReport: monthlyReport

});

const configureStore = () => {
	return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;
