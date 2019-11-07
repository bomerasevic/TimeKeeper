import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import "./EmployeeView.css";
import EmployeesList from "../EmployeesList/EmployeesList";
import Modal from "react-modal";
import * as Yup from "yup";
import swal from "sweetalert";
import axios from "axios";
import { Formik, Form, Field } from "formik";
import { withRouter } from "react-router-dom";


const SignupSchema = Yup.object().shape({
    firstName: Yup.string()
        .max(50, "Too long!")
        .required("Required"),
    lastName: Yup.string()
        .max(50, "Too long!")
        .required("Required"),
    email: Yup.string()
        .email("Invalid email")
        .required("Required"),
    phoneNumber: Yup.string()
        .max(50, "Too long!")
        .required("Required"),
    jobTitle: Yup.string()
    .required("Required"),
    salary: Yup.string()
    .required("Required"),
});
class EmployeeView extends React.Component {
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
        const { classes } = this.props;
        return (
            <div>
                <NavigationLogin />
                <div className="row">
                    <h3 className="table-name">Employees</h3>
                    <a className="btn modal-trigger add-btn" onClick={this.openModal}>
                        Add employee
                    </a>
                </div>
              
                <Modal isOpen={this.state.modalIsOpen} onRequestClose={this.closeModal}>
                  
               
                 <div className="row rowEmployee">             
                  <h1 className="employeeAddh1">Employee Profile</h1>
                  <h2 className="employeeAddh2">
                     Update details
                  </h2>
               </div>
               
                   
               
                  <Formik 
                                            initialValues={{
                                                firstname: "",
                                                lastname: "",
                                                email: "",
                                                phone:"",
                                                birthday: "",
                                                datetime: "",
                                                status: ""
                                            }}
                                            validationSchema={SignupSchema}
                                            onSubmit={(values, { setSubmitting }) => {
                                            }}
                                        >
                                            {({ errors, touched }) => (
                                                 
                                                
                                                <Form>
                                                
                                                 <div className=" col m4 firstColumn">
                                                     
                                                    <div className="input-field">
                                                        <Field
                                                            name="firstName"
                                                            id="firstname"
                                                            type="text"
                                                        />
                                                        {errors.username && touched.firstname ? (
                                                            <div className="errorFirstname">
                                                                {errors.firstname}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="firstname">First name</label>
                                                    </div>

                                                    <div className="input-field">
                                                        <Field
                                                            name="lastname"
                                                            id="lastname"
                                                            type="text"
                                                        />
                                                        {errors.lastname && touched.lastname ? (
                                                            <div className="errorLastname">
                                                                {errors.lastname}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="lastname">Last name</label>
                                                    </div>
                                                    <div className="input-field">
                                                        <Field
                                                            name="email"
                                                            id="email"
                                                            type="email"
                                                        />
                                                        {errors.email && touched.email ? (
                                                            <div className="errorEmail">
                                                                {errors.email}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="email">Email</label>
                                                    </div>
                                                    <div className="input-field">
                                                        <Field
                                                            name="phone"
                                                            id="phone"
                                                            type="text"
                                                        />
                                                        {errors.phone && touched.phone ? (
                                                            <div className="errorPhone">
                                                                {errors.phone}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="phone">Phone</label>
                                                    </div>
                                                    <div className="input-field birthday">
                                                        <Field
                                                            name="date"
                                                            id="date"
                                                            type="date"
                                                        />
                                                        {errors.birthday && touched.birthday ? (
                                                            <div className="errorBirthday">
                                                                {errors.birthday}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="Birthday">Birthday</label>
                                                    </div>
                                                    </div>
                                                    <div className=" col m4 secondColumn" >
                                                    <div className="input-field datetime">
                                                        <Field
                                                            name="datetime"
                                                            id="datetime"
                                                            type="date"
                                                        />
                                                        {errors.datetime && touched.datetime ? (
                                                            <div className="errorDatetime">
                                                                {errors.datetime}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="Datetime">Employee begin date</label>
                                                    </div>
                                                    <div className="input-field datetime">
                                                        <Field
                                                            name="datetime"
                                                            id="datetime"
                                                            type="date"
                                                        />
                                                        {errors.datetime && touched.datetime ? (
                                                            <div className="errorDatetime">
                                                                {errors.datetime}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="Datetime">Employee end date</label>
                                                    </div>
                                                 
                                               </div>
                                              
                                                   
                                                </Form>
                                                
                                              
                                                
                                            )}
                                        </Formik>
                                        
                                        
                </Modal>
                
                 
                <div class="table-employee">
                    <EmployeesList />
                </div>
            </div>
        );
    }
}
export default EmployeeView;
