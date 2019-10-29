import React, { Component } from "react";
import "./Contact.css";
import { Formik, Form, Field } from "formik";

import * as Yup from "yup";

const SignupSchema = Yup.object().shape({
    name: Yup.string()
        .min(5, "Too Short!")
        .required("Required"),
    email: Yup.string()
        .email("Invalid email")
        .required("Required"),
    phoneNumber: Yup.string()
        .min(14, "Too Short!")
        .required("Required"),
    message: Yup.string()
        .min(10, "Must be 10 characters or more")
        .required("Required")
});

const Contact = () => {
    return (
        <div className="contact" id="contact">
            <div className="container">
                <div className="row">
                    <div className="col m4 s12">
                        <h3>Contact us.</h3>
                        <h5>We like to improve</h5>
                    </div>

                    <Formik
                        initialValues={{
                            name: "",
                            phoneNumber: "",
                            email: "",
                            message: ""
                        }}
                        validationSchema={SignupSchema}
                        onSubmit={values => {
                            // same shape as initial values
                            console.log(values);
                        }}
                    >
                        {({ errors, touched }) => (
                            <Form>
                                <div className="col m4 s12">
                                    <div className="input-field">
                                        <Field name="name" id="name" type="text" />
                                        {errors.name && touched.name ? (
                                            <div>{errors.name}</div>
                                        ) : null}
                                        <label htmlFor="name">Name</label>
                                    </div>
                                    <div className="input-field">
                                        <Field name="phoneNumber" id="icon_telephone" type="tel" />
                                        {errors.phoneNumber && touched.phoneNumber ? (
                                            <div>{errors.phoneNumber}</div>
                                        ) : null}
                                        <label htmlFor="icon_telephone">Phone</label>
                                    </div>
                                    <div className="input-field">
                                        <Field name="email" type="email" id="email" />
                                        {errors.email && touched.email ? (
                                            <div>{errors.email}</div>
                                        ) : null}
                                        <label htmlFor="email">Email</label>
                                    </div>
                                </div>
                                <div className="col m4 s12">
                                    <div className="input-field">
                                        <Field
                                            as="textarea"
                                            name="message"
                                            type="message"
                                            className="input-field materialize-textarea"
                                            id="textarea2"
                                            data-length="250"
                                        />
                                        {errors.message && touched.message ? (
                                            <div>{errors.message}</div>
                                        ) : null}
                                        <label htmlFor="textarea2">Message</label>
                                    </div>

                                    <button type="submit" className="waves-effect waves-light btn">
                                        Send message
                                    </button>
                                </div>
                            </Form>
                        )}
                    </Formik>
                </div>
            </div>
        </div>
    );
};

export default Contact;
