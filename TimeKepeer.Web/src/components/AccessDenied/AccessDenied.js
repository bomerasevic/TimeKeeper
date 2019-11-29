import React, { Component } from "react";
import "./AccessDenied.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import Navigation from "../StaticPage/Navigation/Navigation";
import icon from "../../assets/images/computer.svg";
//import FooterBar from "../FooterBar/FooterBar";

//import Menu from "../MobileNavigation/menu";

function AccessDenied() {
    return (
        <div className="header-access " id="home">

            <div className="container header-access-text">
                <img className="logo-large1" src={icon} />
                <h1>Access Denied!</h1>
                <h3>
                    Please contact the Administrator
                </h3>

            </div>

        </div>



    );
}
export default AccessDenied;
