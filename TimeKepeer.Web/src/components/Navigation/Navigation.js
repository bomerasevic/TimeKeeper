import React, { Component } from "react";
import "./Navigation.css";
import logo from "../../assets/images/logo.svg";
import hamburger from "../../assets/images/hamburger.svg";
import Modal from "react-modal";

import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
const SignupSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    password: Yup.string().required("Required")
});
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
                                <a
                                    className="waves-effect waves-light btn modal-trigger"
                                    onClick={this.openModal}
                                >
                                    Login
                                </a>
                                <Modal
                                    isOpen={this.state.modalIsOpen}
                                    onRequestClose={this.closeModal}
                                >
                                    <div className="row">
                                        <h1 className="loginHeader1">Login to your account</h1>

                                        <h2 className="loginHeader2">
                                            Save time for doing great work.
                                        </h2>
                                    </div>
                                    <Formik
                                        initialValues={{
                                            name: "",
                                            password: ""
                                        }}
                                        validationSchema={SignupSchema}
                                        onSubmit={values => {
                                            // same shape as initial values
                                            console.log(values);
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
                                                        <div>{errors.name}</div>
                                                    ) : null}
                                                    <label htmlFor="name">Name</label>
                                                </div>
                                                <div className="input-field">
                                                    <Field
                                                        name="password"
                                                        id="password"
                                                        type="text"
                                                    />
                                                    {errors.password && touched.password ? (
                                                        <div>{errors.password}</div>
                                                    ) : null}
                                                    <label htmlFor="password">password</label>
                                                </div>

                                                <button
                                                    type="submit"
                                                    className="waves-effect waves-light btn"
                                                >
                                                    Send message
                                                </button>
                                            </Form>
                                        )}
                                    </Formik>
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
