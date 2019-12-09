import React, { Component } from "react";
import "./Navigation.css";
import logo from "../../assets/images/logo.svg";
import userLogo from "../../assets/images/user.svg"
import hamburger from "../../assets/images/hamburger.svg";
import Modal from "react-modal";
import swal from "sweetalert";
import axios from "axios";
import * as Yup from "yup";
import { withRouter } from "react-router-dom";
import config from "../../config";
import M from "materialize-css";
import { connect } from "react-redux";
import userManager from "../../utils/userManager"
const SignupSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    password: Yup.string().required("Required")
});
const contactFormEndpoint = "http://192.168.60.72/TimeKeeper/api/users";

class NavigationLogin extends React.Component {
    componentDidMount() {
        let elems = document.querySelectorAll(".dropdown-trigger");
        M.Dropdown.init(elems, {});
        M.AutoInit();
    }

    handleClickEmployees = () => {
        console.log(this.props.user.profile.role);
        this.props.history.push("/app/employees");
    };
    handleClickTeams = () => {
        this.props.history.push("/app/teams");
    };
    handleClickCustomers = () => {
        this.props.history.push("/app/customers");
    };
    handleClickProjects = () => {
        this.props.history.push("/app/projects");
    };
    handleClickTeams = () => {
        this.props.history.push("/app/teams");
    };
    handleClickTracking = () => {
        this.props.history.push("/app/tracking");
    };
    handleClickLogout = () => {
        userManager.signoutRedirect();

    };

    render() {
        return (



            <div className="navbar-fixed">
                {this.props.user ? (
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
                                    <a className="dropdown-trigger hover" data-target="dropdown1">
                                        Database
                                    <i className="fa fa-caret-down" />
                                    </a>

                                    <div className="dropdown-content" id="dropdown1">
                                        <a onClick={this.handleClickEmployees}>Employees</a>
                                        {this.props.user.profile.role === "user" ? null : (
                                            <a onClick={this.handleClickCustomers}>Customers</a>)}

                                        <a onClick={this.handleClickProjects}>Projects</a>
                                        <a onClick={this.handleClickTeams}>Teams</a>
                                    </div>
                                </li>
                                <li>
                                    <a onClick={
                                        this.handleClickTracking}>Time tracking</a>
                                </li>
                                <li>
                                    <a className="dropdown-trigger" data-target="dropdown2">
                                        Reports
                                    <i className="fa fa-caret-down" />
                                    </a>
                                    <div className="dropdown-content" id="dropdown2">
                                        <a href="#">Personal report</a>
                                        <a href="#">Monthly report</a>
                                        <a href="#">Annual report</a>
                                        <a href="#">Project history</a>
                                        <a href="#">Dashboard</a>
                                    </div>
                                </li>
                                <li>
                                    <a className="dropdown-trigger" data-target="dropdown3">
                                        More
                                    <i className="fa fa-caret-down" />
                                    </a>
                                    <div className="dropdown-content" id="dropdown3">
                                        <a href="#">About us</a>
                                        <a href="#">Services</a>
                                        <a href="#">Our staff</a>
                                        <a href="#">Contact</a>
                                    </div>
                                </li>
                                <li>
                                    <a style={{ padding: "0px" }}><img src={userLogo} style={{
                                        height: "30px",
                                        width: "auto", marginTop: "18px"
                                    }}></img></a>
                                </li>
                                <li>
                                    <a style={{ paddingLeft: "10px" }}>{this.props.user.profile.name} ({this.props.user.profile.role})</a>
                                </li>
                                <li>
                                    <a className=" btn modal-trigger" onClick={this.handleClickLogout}>
                                        Log Out
                                </a>
                                </li>
                            </ul>
                        </div>
                    </nav>


                ) : null}
            </div>


        );
    }
}
const mapStateToProps = state => {
    return {
        user: state.user.user
    };
}

export default connect(mapStateToProps, {})(withRouter(NavigationLogin));

