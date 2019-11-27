
import React, { Component } from "react";
import "./Home.css";
import Navigation from "../../StaticPage/Navigation/Navigation";
import logo from "../../../assets/images/logo.svg";
import MobileNavigation from "../../StaticPage/MobileNavigation/MobileNavigation";

//import Menu from "../MobileNavigation/menu";

function Home() {
	return (
		<div className="header " id="home">
			<Navigation />
			<MobileNavigation />
			<div className="container header-text">
				<img className="logo-large" src={logo} />
				<h1>TimeKeeper</h1>
				<h2>
					Save time for doing great work.
					<br />
					Keep progressing. Keep Time.
				</h2>
				<h6>This is the tool for productive people.</h6>
			</div>
		</div>
	);
}
export default Home;
