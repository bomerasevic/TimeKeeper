import React, { Component } from "react";
import "./MobileNavigation.css";
function MobileNavigation() {
    return (
        <ul className="sidenav" id="mobile-demo">
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
    );
}

export default MobileNavigation;
