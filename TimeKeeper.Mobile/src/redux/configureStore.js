import thunk from "redux-thunk";
// import createOidcMiddleware from "redux-oidc";
// import { reducer as oidcReducer } from "redux-oidc";
import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
// import userManager from "../utils/userManager";
import { employeesReducer } from "./reducers/index";
// const oidcMiddleware = createOidcMiddleware(userManager);
const rootReducer = combineReducers({
	employees: employeesReducer
});
const configureStore = () => {
	return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;




