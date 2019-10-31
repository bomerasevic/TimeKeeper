import React, { Component } from "react";
import "./MobileNavigation.css";
import "../Navigation/Navigation.css";
import logomodal from "../../assets/images/logomodal.png";
import Modal from "react-modal";
import swal from "sweetalert";
import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
// Import Materialize
import M from "materialize-css";
const SignupSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    password: Yup.string().required("Required")
});
class MobileNavigation extends Component {
    componentDidMount() {
        // document.addEventListener("DOMContentLoaded", function() {
        let elems = document.querySelectorAll(".sidenav-trigger");
        // 	let instances = M.Sidenav.init(elems, {});
        // });
        M.Sidenav.init(elems, {});
        M.AutoInit();
    }
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
                    <a className="waves-effect waves-light btn" onClick={this.openModal}>
                        Login
                    </a>
                    <Modal
                        className="mobile-login"
                        isOpen={this.state.modalIsOpen}
                        onRequestClose={this.closeModal}
                    >
                        <div className="row mobile-row">
                            <img className="logo-modal" src={logomodal} />

                            <h1 className="loginHeader1">Login to your account</h1>

                            <h2 className="loginHeader2">Save time for doing great work.</h2>
                            <a href="#" class="close" onClick={this.closeModal}></a>
                            <Formik
                                initialValues={{
                                    username: "",
                                    password: ""
                                }}
                                validationSchema={SignupSchema}
                                onSubmit={values => {
                                    swal(
                                        "Login success!",
                                        "",

                                        "success"
                                    );
                                    console.log(values);
                                }}
                            >
                                {({ errors, touched }) => (
                                    <Form>
                                        <div className="input-field">
                                            <Field name="username" id="username" type="text" />
                                            {errors.username && touched.username ? (
                                                <div className="errorUsername">
                                                    {errors.username}
                                                </div>
                                            ) : null}
                                            <label htmlFor="username">username</label>
                                        </div>

                                        <div className="input-field">
                                            <Field name="password" id="password" type="password" />
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
        );
    }
}

export default MobileNavigation;
