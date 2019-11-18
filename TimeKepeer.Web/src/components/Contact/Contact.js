import React, { useState } from "react";
import "./Contact.css";
import { Formik, Form, Field } from "formik";
import axios from "axios";
import * as Yup from "yup";
import swal from "sweetalert";
const phoneRegExp = /^[0-9]/;
const SignupSchema = Yup.object().shape({
    name: Yup.string()
        .max(50, "Too long!")
        .required("Required"),
    email: Yup.string()
        .email("Invalid email")
        .required("Required"),
    phoneNumber: Yup.string()
        .max(50, "Too long!")
        .required("Required"),
    message: Yup.string()
        .min(10, "Must be 10 characters or more")
        .required("Required"),

    phoneNumber: Yup.string().matches(phoneRegExp, 'Phone number is not valid'),
    message: Yup.string()
        .min(10, "Must be 10 characters or more")
        .required("Required")
});

const contactFormEndpoint = "http://192.168.60.72/TimeKeeper/api/contact";
const Contact = () => {
    const [isSubmitionCompleted, setSubmitionCompleted] = useState(false);
    function handleClickOpen() {
        setSubmitionCompleted(false);
    }
    return (
        <div className="contact" id="contact">
            {!isSubmitionCompleted && (
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
                            onSubmit={(values, { setSubmitting }) => {
                                setSubmitting(true);
                                axios
                                    .post(contactFormEndpoint, values, {
                                        headers: {
                                            "Access-Control-Allow-Origin": "*",
                                            "Content-Type": "application/json"
                                        }
                                    })
                                    .then(resp => {
                                        console.log(resp);
                                        setSubmitionCompleted(true);
                                    })
                                    .catch(err => {
                                        console.log(err);
                                        swal(
                                            "Wrong password",
                                            "",
                                            "error"
                                        );
                                        setSubmitionCompleted(false);
                                    });
                            }}
                        >
                            {({ errors, touched }) => (
                                <Form>
                                    <div className="col m4 s12">
                                        <div className="input-field static">
                                            <Field name="name" id="name" type="text" />
                                            {errors.name && touched.name ? (
                                                <div>{errors.name}</div>
                                            ) : null}
                                            <label htmlFor="name">Name</label>
                                        </div>
                                        <div className="input-field static">
                                            <Field
                                                className="validate"
                                                name="phoneNumber"
                                                id="icon_telephone"
                                                type="tel"
                                            />
                                            {errors.phoneNumber && touched.phoneNumber ? (
                                                <div>{errors.phoneNumber}</div>
                                            ) : null}
                                            <label htmlFor="icon_telephone">Phone</label>
                                        </div>
                                        <div className="input-field static">
                                            <Field name="email" type="email" id="email1" />
                                            {errors.email && touched.email ? (
                                                <div>{errors.email}</div>
                                            ) : null}
                                            <label htmlFor="email">Email</label>
                                        </div>
                                    </div>
                                    <div className="col m4 s12">
                                        <div className="input-field static">
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

                                        <button
                                            type="submit"
                                            className=" btn"
                                            onClick={handleClickOpen}
                                        >
                                            Send message
                                        </button>
                                    </div>
                                </Form>
                            )}
                        </Formik>
                    </div>
                </div>
            )}
            {isSubmitionCompleted && (
                <div className="after-message">
                    <h3>Thank you for contacting us!</h3>
                    <h5>We will be in touch soon.</h5>
                </div>
            )}
        </div>
    );
};

export default Contact;
