import React, { Component } from "react";
import "./Welcome.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import Navigation from "../StaticPage/Navigation/Navigation";
import logo from "../../assets/images/puzzle.png";

import { connect } from "react-redux";
//import FooterBar from "../FooterBar/FooterBar";

//import Menu from "../MobileNavigation/menu";

function Welcome(props) {
	return (
		<div className="header-welcome " id="home">
			{props.user ? (
				<div>
					< NavigationLogin />
					<div className="container header-text">
						<img className="logo-large" src={logo} />
						<h1 style={{ padding: "0", margin: "0" }}>Welcome </h1>
						<h1 style={{ fontSize: "40px", padding: "0", margin: "0" }}>{props.user.profile.name} </h1>
						<h2 style={{ padding: "0", margin: "0" }}>({props.user.profile.role})</h2>
						<h2 style={{ fontSize: "10x" }}>
							This is your TimeKeeper.

						</h2>

						<h6>Keep doing a great job.</h6>
					</div>
				</div>


			) : null}

		</div>
	);
}
const mapStateToProps = state => {
	return {
		user: state.user.user
	};
}
export default connect(mapStateToProps, {})(Welcome);
