import React from "react";
import ReactDOM from "react-dom";

import { BrowserRouter } from "react-router-dom";
 import { Provider } from "react-redux";
 import { AuthProvider } from "./providers/authProvider";
 import {  createStore, applyMiddleware } from "redux";


import thunk from "redux-thunk";
 import { composeWithDevTools } from "redux-devtools-extension";

import App from "./App";
 import reducers  from "./store/reducers";


const store = createStore(
  reducers,
  composeWithDevTools(applyMiddleware(thunk))
);

ReactDOM.render(
	<Provider store={store}>
		<AuthProvider>
			<BrowserRouter>
				<App />
			</BrowserRouter>
		</AuthProvider>
	</Provider>,
	document.getElementById("root")
);
