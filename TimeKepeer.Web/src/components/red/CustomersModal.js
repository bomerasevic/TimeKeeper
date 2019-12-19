import React, {useEffect} from "react";
import { withStyles } from "@material-ui/core/styles";
import { connect } from 'react-redux'
import { Formik, Field, Form } from "formik";
import * as Yup from "yup";
import moment from "moment";

import {
  Input,
  Dialog,
  DialogContent,
  Button,
  InputLabel,
  Select,
  MenuItem,
  Table, TableBody, TableCell, TableHead,TableRow, Paper 
} from "@material-ui/core";
import styles from './EmployeesModalStyle';
import {fetchCustomer, customerCancel, customerPut, customerAdd, customerDelete} from '../../store/actions/index';
 

const statuses = [
  { id: 1, name: "Prospect" },
  { id: 2, name: "Client" }
];

const team = membersData => {
	let index = membersData.indexOf(",");
	let team = membersData.substr(index + 1);
  
	return team;
  };

const Schema = Yup.object().shape({
 
  businessName: Yup.string()
    .min(2, "Business Name too short!")
    .max(32, "Business Name too long!")
    .required("Business Name is Required!"),
  contactName: Yup.string()
    .min(2, "Contact Name too short!")
    .max(32, "Contact Name too long!")
    .required("Contat Name is Required!"),
  email: Yup.string().required("Email is Required!"),
  Address: Yup.string().required("Home Address is Required!"),
  ZipCode: Yup.string().required("Zip Code is Required!"),

  city: Yup.string().required("City is Required!"),
  status: Yup.string().required("Status is Required!"),
  project: Yup.string().required("Project is Required!")
});




const CustomersModal = (props) => {
    const { classes, open, customer, id, mode } = props;
    const { fetchCustomer, customerCancel, customerPut, customerAdd } = props;
    let rows = []

    if(customer) {
     
        let fetchedName = [];
        console.log(customer);
        customer.members.forEach(r => {

          let team = test(r.name);
          let id = r.id;
          let data = { id, team };
          fetchedName.push(data);
        });
        rows = fetchedName
    }
   
    useEffect(() => {
      if(id !== null) {
        fetchCustomer(id)
      }
      
    }, [])

  
    const CustomInputComponent = props => (
      <Input
        // required={true}
        disabled={mode === 'view' ? true : null}
        fullWidth={true}
        className={classes.input}
        {...props}
      />
    );
    

    
    const CustomStatusComponent = props => {
      return (
        <Select fullWidth {...props} className={classes.input} disabled={mode === 'view' ? true : null}>
          <MenuItem value={1}>Prospect</MenuItem>
          <MenuItem value={2}>Client</MenuItem>
       
        </Select>
      );
    };

    return (
      <React.Fragment>
       
          {customer || mode==='add' ? <Formik
            validationSchema={Schema}
            initialValues={{
             
              businessName: customer ? customer.businessName : "",
              contactName: customer ? customer.contactName : "",
              email: customer ? customer.email : "",
              address: customer ? customer.homeAddress : "",
            
              status: customer ? customer.status.id : ""
            }}
            onSubmit={values => {
           

              //let newPosition = positions.filter(p => values.position === p.id);
              let newStatus = statuses.filter(s => values.status === s.id);

              //values.position = newPosition[0];
              values.status = newStatus[0];

              if(customer) {
                values.id = customer.id
                customerPut(customer.id, values)
              } else {
                values.image = 'image'
                customerAdd(values)
              }

         
            }}
          >
            {({ errors, touched }) => (
              <Dialog
                open={open}
                aria-labelledby="form-dialog-title"
                maxWidth="lg"
              >
                <DialogContent>
                  <Form>
                    <div className={classes.wrapper}>
                      <div className={classes.container}>
                        <img
                          src="http://www.hmcatering.com/wp-content/uploads/2015/05/profile-placeholder.jpg"
                          alt="image"
                          className={classes.img}
                        />
                        
                        <InputLabel>Business Name</InputLabel>
                        {errors.businessName && touched.businessName ? (
                          <div className={classes.errorMessage}>
                            {errors.businessName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="businessName"
                          disableUnderline={true}
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        
                      </div>

                      <div className={classes.container}>
                    
                        <InputLabel>Contact Name</InputLabel>
                        {errors.contactName && touched.contactName ? (
                          <div className={classes.errorMessage}>
                            {errors.contactName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="contactName"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                        <InputLabel>e-Mail</InputLabel>
                        {errors.email && touched.email ? (
                          <div className={classes.errorMessage}>
                            {errors.email}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="email"
                          type="email"
                          disableUnderline={true}
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Home Number</InputLabel>
                        {errors.address && touched.address ? (
                          <div className={classes.errorMessage}>
                            {errors.address}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="address"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                        <InputLabel>Zip Code</InputLabel>
                        {errors.zipCode && touched.zipCode ? (
                          <div className={classes.errorMessage}>
                            {errors.zipCode}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="zipCode"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                      </div>
                      <div className={classes.container}>
                        <InputLabel>City</InputLabel>
                        {errors.city && touched.city ? (
                          <div className={classes.errorMessage}>
                            {errors.city}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="city"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                        <InputLabel>Status</InputLabel>
                        {errors.status && touched.status ? (
                          <div className={classes.errorMessage}>
                            {errors.status}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="status"
                          as={CustomStatusComponent}
                        />
                        <InputLabel>Team</InputLabel>
                        {errors.team && touched.team ? (
                          <div className={classes.errorMessage}>
                            {errors.team}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="team"
                          autoComplete="off"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                       <InputLabel>Project</InputLabel>
                        {errors.project && touched.project ? (
                          <div className={classes.errorMessage}>
                            {errors.project}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="project"
                          autoComplete="off"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                        <div className={classes.buttons}>
                          {mode !== 'view' ? <Button className={classes.submitButton}
                            variant="contained"
                            //color="primary"
                            type="submit"
                          >
                            Submit
                          </Button> : null}
                          <Button
                            variant="contained"
                            //color={mode === 'view' ? 'primary' : 'secondary'}
                            onClick={() => customerCancel()}
                          >
                            {mode==='view' ? 'Back' : "Cancel"}
                          </Button>
                        </div>
                      </div>
                    </div>
                  </Form>
                </DialogContent>
              </Dialog>
            )}
          </Formik> : null}
  
      </React.Fragment>
    );
  }


const mapStateToProps = state => {
  return {
    id: state.customer.selected.id,
    customer: state.customers.customer,
    mode: state.customers.selected.mode
  }
}

export default connect(mapStateToProps, {fetchCustomer, customerCancel, customerPut, customerAdd})(withStyles(styles)(CustomersModal));