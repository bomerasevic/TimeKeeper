
import React, { Component } from "react";
import "./Home.css";
import Navigation from "../../StaticPage/Navigation/Navigation";
import logo from "../../../assets/images/";
import MobileNavigation from "../../StaticPage/MobileNavigation/MobileNavigation";

//import Menu from "../MobileNavigation/menu";

function Home() {
	return (
		<div className="header " id="home">
			<Navigation />
			<MobileNavigation />
			<div className="container header-text">
				<img className="logo-large" src={logo} />
				<h1>STUDINGO</h1>
				<h2>
					Vodič za studente.
				</h2>
				<h6>Olakšajte svoj studentski život.
</h6>
			</div>
		</div>
	);
}
export default Home;
