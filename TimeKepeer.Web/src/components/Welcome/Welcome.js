import React, { Component } from "react";
import "./Welcome.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import Navigation from "../StaticPage/Navigation/Navigation";
import logo from "../../assets/images/puzzle.png";
//import FooterBar from "../FooterBar/FooterBar";

//import Menu from "../MobileNavigation/menu";

function Welcome() {
	return (
		<div className="header-welcome " id="home">
			<NavigationLogin />
			<div className="container header-text">
				<img className="logo-large" src={logo} />
				<h1>Welcome</h1>
				<h2>
					This is your TimeKeeper.
					<br />
					Take your time.
				</h2>
				<h6>Keep doing a great job.</h6>
			</div>
		</div>
	);
}
export default Welcome;