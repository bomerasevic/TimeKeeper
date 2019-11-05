import React, { Component } from "react";
import "../NavigationLogin/Navigation.css";
import logo from "../../assets/images/logo.svg";
import logomodal from "../../assets/images/logomodal.png";
import hamburger from "../../assets/images/hamburger.svg";
import Modal from "react-modal";
import swal from "sweetalert";
import axios from "axios";
import * as Yup from "yup";
import { withRouter } from "react-router-dom";

import M from "materialize-css";
const SignupSchema = Yup.object().shape({
	username: Yup.string().min(5, "Too Short!").required("Required"),
	password: Yup.string().required("Required")
});
const contactFormEndpoint = "http://192.168.60.72/TimeKeeper/api/users";

class FooterBar extends React.Component {
    
   

	render() {
		return (
			<div className="navbar-fixed">
				<nav className="custom-navbar ">
					<div className="nav-wrapper">
						<a href="#home" className="left brand-logo">
							<img id="time-keeper-logo" src={logo} />
						</a>
						<a
							href="#"
							data-target="mobile-demo"
							className="sidenav-trigger button-collapse"
						>
							<i className="material-icons">
								<img src={hamburger} />
							</i>
						</a>
						<ul className="right hide-on-med-and-down">
                        <li>
								<a href="#services">About us</a>
							</li>
                            <li>
								<a href="#services">Services</a>
							</li>
                            <li>
								<a href="#services">Our staff</a>
							</li>
                            <li>
								<a href="#services">Contact us</a>
							</li>
                            
							
						</ul>
					</div>
				</nav>
			</div>
		);
	}
}

export default withRouter(FooterBar);
