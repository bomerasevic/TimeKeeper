import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import "./ProjectView.css";
import ProjectList from "../ProjectsList/ProjectList";
import Modal from "react-modal";
import Button from "@material-ui/core/Button";
import * as Yup from "yup";
import swal from "sweetalert";
import axios from "axios";
import { Formik, Form, Field } from "formik";
import { withRouter } from "react-router-dom";

const SignupSchema = Yup.object().shape({
    projectname: Yup.string()
        .max(50, "Too long!")
        .required("Required!"),
    description: Yup.string()
        .max(250, "Too long!")
    
  
   
});
class ProjectView extends React.Component {
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
                    <h3 className="table-name">Projects</h3>
                    <a className="btn add-btn" onClick={this.openModal}>
                        Add project
                    </a>
                </div>
              
                <Modal isOpen={this.state.modalIsOpen} onRequestClose={this.closeModal}>
                  
               
                 <div className="row rowProject">             
                  <h1 className="projectAddh1">Project Screen</h1>
                  <h2 className="projectAddh2">
                     Project Details
                  </h2>
               </div>
               
                   
               
                  <Formik 
                                            initialValues={{
                                                projectname: "",
                                                description: "",
                                                startdatetime: "",
                                                enddatetime: "",
                                                status:"",
                                                team:"",
                                                customer: "",
                                                pricing:""
                                             
                                                
                                            }}
                                            validationSchema={SignupSchema}
                                            onSubmit={(values, { setSubmitting }) => {
                                            }}
                                        >
                                            {({ errors, touched }) => (
                                                 
                                                
                                                <Form>
                                                
                                                 <div className=" col m4 firstColumnProject">
                                                 <div className="input-field project-name">
                                                        <Field
                                                            name="projectName"
                                                            id="projectname"
                                                            type="text"
                                                        />
                                                        {errors.projectname && touched.projectname ? (
                                                            <div className="errorProjectname">
                                                                {errors.projectname}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="projectname">Project Name</label>
                                                    </div>
                                                    <div className="input-field">
                                                        <Field
                                                           
                                                           as="textarea"
                                                           name="description"
                                                           type="description"
                                                           className="input-field materialize-textarea"
                                                           id="textarea2"
                                                         
                                                            
                                                        />
                                                        {errors.description && touched.description ? (
                                                            <div className="errorDescription">
                                                                {errors.description}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="textarea">Describe role</label>
                                                    </div>
                                                  
                                                  
                                                       </div>
                                                       <div className=" col m4 secondColumnProject">

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
                                                        <label  htmlFor="Datetime"> Start date</label>
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
                                                        <label  htmlFor="Datetime">End date</label>
                                                    </div>
                                                    <div className="input-field select-dropdown">
                                                        <datalist id="status">
                                                        <option>Client</option>
                                                        <option>Interested</option>
                                                        <option>Not a client</option>
                                                        </datalist>
                                                        <Field
                                                            name="statusClient"
                                                            id="client"
                                                            type="text"
                                                            list="status"
                                                            
                                                        />
                                                        {errors.select && touched.select ? (
                                                            <div className="errorSelect">
                                                                {errors.select}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="client">Status</label>
                                                    </div>
                                                    <div className="input-field  select-dropdown" >
                                                    <datalist  id="team">
                                                        <option>Alpha</option>
                                                        <option>Bravo</option>
                                                        <option>Charlie</option>
                                                        <option>Delta</option>
                                                       
                                                        </datalist>
                                                        <Field readOnly={this.state.readOnly}
                                                        
                                                            name="team"
                                                            id="team"
                                                           //type="text"
                                                            list="team"
                                                        />
                                                        {errors.team && touched.team ? (
                                                            <div className="errorTeam">
                                                                {errors.team}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="select-dropdown">Team</label>
                                                    </div>
                                                           </div>

                                                           <div className=" col m4 thirdColumnProject">

                                                           <div className="input-field">
                                                            <Field 
                                                            name="customer"
                                                            id="customer"
                                                            type="text"
                                                           />
                                                        {errors.customer && touched.customer ? (
                                                            <div className="errorProjectname">
                                                                {errors.customer}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="customer">Customer</label>
                                                    </div>
                                                    <div className="input-field select-dropdown disabled">
                                                        <datalist id="pricing">
                                                        <option>Fixed bid</option>
                                                        <option>Hourly rate</option>
                                                      
                                                        </datalist>
                                                        <Field
                                                            name="pricing"
                                                            id="pricing"
                                                            type="text"
                                                            list="pricing"
                                                            
                                                        />
                                                        {errors.select && touched.select ? (
                                                            <div className="errorSelect">
                                                                {errors.select}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="pricing">Pricing</label>
                                                    </div>
                                                    <div className="input-field select-dropdown">
                                                        <datalist id="fixedbid">
                                                        <option>Yes</option>
                                                        <option>No</option>
                                                      
                                                        </datalist>
                                                        <Field
                                                         
                                                            name="fixedbid"
                                                            id="fixedbid"
                                                            type="text"
                                                            list="fixedbid"
                                                            
                                                        />
                                                        {errors.fixedbid && touched.fixedbid ? (
                                                            <div className="errorSelect">
                                                                {errors.fixedbid}
                                                            </div>
                                                        ) : null}
                                                        <label  htmlFor="fixed-bid">Fixed Bid</label>
                                                    </div>
                                                    <div className="buttonsProject">
                                                        <button className="btn" id="update">UPDATE</button>
                                                        <button className="btn" id="close">CLOSE</button>

                                                    </div>
                                                               </div>
                                                </Form>
                                                
                                              
                                                
                                            )}
                                        </Formik>
                                        
                                        
                </Modal>
                
                 
                <div class="table-employee">
                    <ProjectList />
                </div>
            </div>
        );
    }
}
export default ProjectView;
