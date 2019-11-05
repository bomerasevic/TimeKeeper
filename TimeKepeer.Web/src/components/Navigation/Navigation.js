import React, { Component } from "react";
import "./Navigation.css";
import logo from "../../assets/images/logo.svg";
import logomodal from "../../assets/images/logomodal.png";
import hamburger from "../../assets/images/hamburger.svg";
import Modal from "react-modal";
import swal from "sweetalert";
import axios from "axios";
import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
const SignupSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    password: Yup.string().required("Required")
});
const contactFormEndpoint = "http://192.168.60.72/TimeKeeper/api/users";

class Navigation extends React.Component {
    constructor() {
        super();
        this.state = {
            modalIsOpen: false
        };
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }
    openModal() {
        this.setState({ modalIsOpen: true });
    }
    closeModal() {
        this.setState({ modalIsOpen: false });
    }
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
                                <a className=" btn modal-trigger" onClick={this.openModal}>
                                    Login
                                </a>
                                <Modal
                                    isOpen={this.state.modalIsOpen}
                                    onRequestClose={this.closeModal}
                                >
                                    <div className="row">
                                        <img className="logo-modal" src={logomodal} />

                                        <h1 className="loginHeader1">Login to your account</h1>

                                        <h2 className="loginHeader2">
                                            Save time for doing great work.
                                        </h2>
                                        <a href="#" class="close" onClick={this.closeModal}></a>
                                        <Formik
                                            initialValues={{
                                                username: "",
                                                password: ""
                                            }}
                                            validationSchema={SignupSchema}
                                            onSubmit={(values, { setSubmitting }) => {
                                                axios
                                                    .post(contactFormEndpoint, values, {
                                                        headers: {
                                                            "Access-Control-Allow-Origin": "*",
                                                            "Content-Type": "application/json"
                                                        }
                                                    })
                                                    .then(resp => {
                                                        swal("Login success", "", "success");
                                                        console.log(resp);
                                                    })
                                                    .catch(err => {
                                                        console.log(err);
                                                        swal(
                                                            "Oops...",
                                                            "Something went wrong! Reload page.",
                                                            "error"
                                                        );
                                                    });
                                            }}
                                        >
                                            {({ errors, touched }) => (
                                                <Form>
                                                    <div className="input-field">
                                                        <Field
                                                            name="username"
                                                            id="username"
                                                            type="text"
                                                        />
                                                        {errors.username && touched.username ? (
                                                            <div className="errorUsername">
                                                                {errors.username}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="username">username</label>
                                                    </div>

                                                    <div className="input-field">
                                                        <Field
                                                            name="password"
                                                            id="password"
                                                            type="password"
                                                        />
                                                        {errors.password && touched.password ? (
                                                            <div className="errorPassword">
                                                                {errors.password}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="password">password</label>
                                                    </div>
                                                    <div id="loginbtn">
                                                        <button type="submit" className=" btn ">
                                                            LOGIN
                                                        </button>
                                                    </div>
                                                </Form>
                                            )}
                                        </Formik>
                                    </div>
                                </Modal>
                            </li>
                        </ul>
                    </div>
                </nav>
            </div>
        );
    }
}

export default Navigation;
