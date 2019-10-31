import React from "react";
import "./MobileLogin.css";
import logomodal from "../../assets/images/logomodal.png";

import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
const SignupSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    password: Yup.string().required("Required")
});
class MobileLogin extends React.Component {
    render() {
        console.log(this.props.serviceTitle);
        return (
            <div className="row mobile-login">
                <img className="logo-modal" src={logomodal} />

                <h1 className="loginHeader1">Login to your account</h1>

                <h2 className="loginHeader2">Save time for doing great work.</h2>
                <Formik
                    initialValues={{
                        username: "",
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
                                <Field name="username" id="username" type="text" />
                                {errors.username && touched.username ? (
                                    <div className="errorUsername">{errors.username}</div>
                                ) : null}
                                <label htmlFor="username">username</label>
                            </div>

                            <div className="input-field">
                                <Field name="password" id="password" type="text" />
                                {errors.password && touched.password ? (
                                    <div className="errorPassword">{errors.password}</div>
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
        );
    }
}
export default MobileLogin;
