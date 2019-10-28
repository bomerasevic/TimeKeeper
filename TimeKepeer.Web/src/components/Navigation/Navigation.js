import React, { Component } from "react";
import "./Navigation.css";
import logo from "../../assets/images/logo.svg";
import hamburger from "../../assets/images/hamburger.svg";

function Navigation() {
    return (
        <div className="navbar-fixed">
            <nav className="custom-navbar ">
                <div className="nav-wrapper">
                    <a href="#home" className="left brand-logo">
                        <img id="time-keeper-logo" src={logo} />
                    </a>
                    <a href="#" data-target="mobile-demo" className="sidenav-trigger button-collapse">
                        <i className="material-icons">
                            <img src={hamburger} />
                        </i>
                    </a>
                    <ul className="right hide-on-med-and-down">
                        <li>
                            <a href="#about">About us</a>
                        </li>
                        <li>
                            <a href="#services">Services</a>
                        </li>
                        <li>
                            <a href="#staff">Our staff</a>
                        </li>
                        <li>
                            <a href="#contact">Contact us</a>
                        </li>
                        <li>
                            <a className="waves-effect waves-light btn">Login</a>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
    );
}

export default Navigation;
