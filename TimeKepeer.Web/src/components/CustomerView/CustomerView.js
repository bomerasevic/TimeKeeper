import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import "./CustomerView.css";
import CustomerList from "../CustomerList/CustomerList";
import Modal from "react-modal";
import Button from "@material-ui/core/Button";
import * as Yup from "yup";
import swal from "sweetalert";
import axios from "axios";
import { Formik, Form, Field } from "formik";

import { withRouter } from "react-router-dom";

const SignupSchema = Yup.object().shape({
    businessname: Yup.string()
        .max(50, "Too long!")
        .required("Required!"),
    contactname: Yup.string()
        .max(50, "Too long!")
        .required("Required!"),
    email: Yup.string()
        .email("Invalid email")
        .required("Required!"),
   
});
class CustomerView extends React.Component {
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
                    <h3 className="table-name">Customers</h3>
                    <a className="btn modal-trigger add-btn" onClick={this.openModal}>
                        Add customer
                    </a>
                </div>
              
                <Modal isOpen={this.state.modalIsOpen} onRequestClose={this.closeModal}>
                  
               
                 <div className="row rowCustomer">             
                  <h1 className="customerAddh1">Customer Profile</h1>
                  <h2 className="customerAddh2">
                     Update details
                  </h2>
               </div>
               
                   
               
                  <Formik 
                                            initialValues={{
                                                businessname: "",
                                                contactname: "",
                                                email:"",
                                                zipcode: "",
                                                city:"",
                                                status:"",
                                                team:"",
                                                project:"",
                                              
                                                
                                            }}
                                            validationSchema={SignupSchema}
                                            onSubmit={(values, { setSubmitting }) => {
                                            }}
                                        >
                                            {({ errors, touched }) => (
                                                 
                                                
                                                <Form>
                                                
                                                 <div className=" col m4 firstColumnCustomer">
                                                     
                                                 <div className="imgBoxCustomer">
                                                 <button className="btn upload">Add image</button>
                                                  </div>
                                                  
                                                  <div className="input-field business">
                                                        <Field
                                                            name="businessName"
                                                            id="businessname"
                                                            type="text"
                                                        />
                                                        {errors.businessname && touched.businessname ? (
                                                            <div className="errorBusinessname">
                                                                {errors.businessname}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="businessname">Business Name</label>
                                                    </div>

                                               </div>
                                               <div className=" col m4 secondColumnCustomer">
                                               <div className="input-field customer">
                                                        <Field
                                                            name="contactName"
                                                            id="contactname"
                                                            type="text"
                                                        />
                                                        {errors.contactname && touched.contactname ? (
                                                            <div className="errorContactname">
                                                                {errors.contactname}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="contactname">Contact Name</label>
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
                                                            name="address"
                                                            id="address"
                                                            type="text"
                                                        />
                                                        {errors.address && touched.address ? (
                                                            <div className="errorAddress">
                                                                {errors.address}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="address">Home Address</label>
                                                    </div>
                                                    <div className="input-field">
                                                        <Field
                                                            name="zipCode"
                                                            id="zipcode"
                                                            type="text"
                                                        />
                                                        {errors.zipcode && touched.zipcode ? (
                                                            <div className="errorZipcode">
                                                                {errors.zipcode}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="zipcode">Zip Code</label>
                                                    </div>

                                                   </div>
                                                   <div className=" col m4 thirdColumnCustomer">
                                                   <div className="input-field">
                                                        <Field
                                                            name="city"
                                                            id="city"
                                                            type="text"
                                                        />
                                                        {errors.city && touched.city ? (
                                                            <div className="errorCity">
                                                                {errors.city}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="city">City</label>
                                                    </div>
                                                    <div className="input-field select-dropdown">
                                                        <datalist id="status">
                                                        <option>Client</option>
                                                        <option>Interested</option>
                                                        <option>Not a client</option>
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
                                                            name="project"
                                                            id="project"
                                                            type="text"
                                                        />
                                                        {errors.project && touched.project ? (
                                                            <div className="errorProject">
                                                                {errors.project}
                                                            </div>
                                                        ) : null}
                                                        <label htmlFor="project">Project</label>
                                                    </div>
                                                    <div className="buttonsCustomer">
                                                        <button className="btn" id="update">UPDATE</button>
                                                        <button className="btn" id="close">CLOSE</button>

                                                    </div>
                                                       </div>

                                                   
                                                </Form>
                                                
                                              
                                                
                                            )}
                                        </Formik>
                                        
                                        
                </Modal>
                
                 
                <div class="table-employee">
                    <CustomerList />
                </div>
            </div>
            
             
        );
    }
}
export default CustomerView;