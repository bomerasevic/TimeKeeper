import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import "./EmployeeView.css";
import EmployeesList from "../EmployeesList/EmployeesList";
import Modal from "react-modal";
import Button from "@material-ui/core/Button";
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
                                                startdatetime: "",
                                                enddatetime: "",
                                                status: "",
                                                jobtitle: "",
                                                salary:"",
                                                team: "",
                                                textarea2:""
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
                                                    <div className="input-field select-dropdown">
                                                        <datalist id="status">
                                                        <option>Intern</option>
                                                        <option>Employed</option>
                                                        <option>Leaver</option>
                                                        </datalist>
                                                        <Field
                                                            name="select"
                                                            id="select"
                                                            type="text"
                                                            list="status"
                                                            
                                                        />
                                                        {errors.select && touched.select ? (
                                                            <div className="errorSelect">
                                                                {errors.select}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="select-dropdown">Status</label>
                                                    </div>
                                                    <div className="input-field datetime">
                                                        <Field
                                                            name="startdatetime"
                                                            id="startdatetime"
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
                                                            name="enddatetime"
                                                            id="enddatetime"
                                                            type="date"
                                                        />
                                                        {errors.datetime && touched.datetime ? (
                                                            <div className="errorDatetime">
                                                                {errors.datetime}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="Datetime">Employee end date</label>
                                                    </div>
                                                    <div className="input-field select-dropdown">
                                                    <datalist id="jobtitle">
                                                        <option>DEV</option>
                                                        <option>UI/UX</option>
                                                        <option>QAE</option>
                                                        <option>MGR</option>
                                                        <option>HRM</option>
                                                        <option>CEO</option>
                                                        <option>CTO</option>
                                                        <option>COO</option>
                                                        </datalist>
                                                        <Field
                                                            name="jobtitle"
                                                            id="jobtitle"
                                                            type="text"
                                                            list="jobtitle"
                                                        />
                                                        {errors.jobtitle && touched.jobtitle ? (
                                                            <div className="errorJobtitle">
                                                                {errors.jobtitle}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="select-dropdown">Job Title</label>
                                                    </div>

                                                    <div className="input-field">
                                                        <Field
                                                            name="salary"
                                                            id="salary"
                                                            type="number"
                                                        />
                                                        {errors.salary && touched.salary ? (
                                                            <div className="errorSalary">
                                                                {errors.salary}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="salary">Contracted salary</label>
                                                    </div>
                                               </div>
                                               <div className=" col m4 thirdColumn" >

                                                  <div className="imgBox">
                                                      <button className="btn upload">Add image</button>
                                                  </div>
                                                  <div className="input-field select-dropdown">
                                                    <datalist id="team">
                                                        <option>Alpha</option>
                                                        <option>Bravo</option>
                                                        <option>Charlie</option>
                                                        <option>Delta</option>
                                                       
                                                        </datalist>
                                                        <Field
                                                            name="team"
                                                            id="team"
                                                            type="text"
                                                            list="team"
                                                        />
                                                        {errors.team && touched.team ? (
                                                            <div className="errorTeam">
                                                                {errors.team}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="select-dropdown">Team</label>
                                                    </div>
                                                    <div className="input-field">
                                                        <Field
                                                           
                                                            name="input-field materialize-textarea"
                                                            id="description"
                                                            type="text"
                                                            
                                                        />
                                                        {errors.description && touched.description ? (
                                                            <div className="errorDescription">
                                                                {errors.description}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="description">Describe role</label>
                                                    </div>
                                                    <div className="buttonsEmployee">
                                                        <button className="btn" id="update">UPDATE</button>
                                                        <button className="btn" id="close">CLOSE</button>

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
