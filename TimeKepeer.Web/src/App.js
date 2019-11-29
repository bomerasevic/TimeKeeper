import React, { Component } from "react";
import "./App.css";
import "materialize-css/dist/css/materialize.min.css";
import Home from "./components/StaticPage/Home/Home";
import AboutUs from "./components/StaticPage/AboutUs/AboutUs";
import { connect } from "react-redux";
import Team from "./components/StaticPage/Team/Team";
import Slider from "react-slick";
import Services from "./components/StaticPage/Service/Services";
import Contact from "./components/StaticPage/Contact/Contact";
import Footer from "./components/StaticPage/Footer/Footer";
import Welcome from "./components/Welcome/Welcome";
import { BrowserRouter, Switch, Route, withRouter } from "react-router-dom";
import TeamsView from "./components/TeamsPage/TeamsPage";
import TrackingView from "./components/TeamTimeTracking/TeamTimeTracking";
import EmployeesPage from "./components/red/EmployeesPage";
import ProjectsPage from "./components/red/ProjectsPage";
import CustomersPage from "./components/red/CustomersPage";
import Callback from "./components/LoginCallback";
import AccessDenied from "./components/AccessDenied/AccessDenied";


//import EmployeesView from "./components/EmployeesView/EmployeesView";
//import ProjectsView from "./components/ProjectsView/ProjectView";
//import CustomersView from "./components/CustomersView/CustomersView";


class App extends React.Component {
	render() {
		return (
			<BrowserRouter>

				<Switch>
					<Route exact path="/auth-callback" component={Callback} />
					<Route exact path="/">
						<div className="App">
							<Home />
							<AboutUs />
							<Services />
							<Slider />
							<Team />
							<Contact />
							<Footer />
						</div>
					</Route>
					
					<Route exact={true} path="/auth-callback" component={Callback} />
					<Route exact path="/app">
						<Welcome />
					</Route>
					
					<Route exact path="/app/employees">
						<EmployeesPage />
					</Route>
					<Route exact path="/app/projects">
						<ProjectsPage />
					</Route>
					<Route exact path="/app/customers">
						<CustomersPage />

					</Route>

					<Route exact path="/app/teams">
						<TeamsView />
					</Route>
					<Route exact path="/app/tracking">
						<TrackingView />
					</Route>


					<Route exact path="/app/access">
						<AccessDenied />
					</Route>
				</Switch>
			</BrowserRouter>


		);
	}
}

// const mapStateToProps = (state) => {
// 	return { user: state.user };
// };

// export default connect(mapStateToProps)(withRouter(App));

export default withRouter(App);
