import React, { useEffect } from "react";
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
  Table, TableBody, TableCell, TableHead, TableRow, Paper
} from "@material-ui/core";
import styles from './EmployeesModalStyle';
import { fetchCustomer, customerCancel, customerPut, customerAdd } from '../../store/actions/index';


const team = [
  { id: 1, name: "Alpha" },
  { id: 2, name: "Bravo" },
  { id: 3, name: "Charlie" },
  { id: 4, name: "Delta" },
  { id: 5, name: "Foxtrot" },
  { id: 6, name: "Oscar" },
  { id: 7, name: "Sierra" },
  { id: 8, name: "Tango" },
  {id: 9, name: "Yankee"}
];

const statuses = [
  { id: 1, name: "Prospect" },
  { id: 2, name: "Client" },

];

const test = (membersData) => {
  let index = membersData.indexOf(",");
  let team = membersData.substr(index + 1);

  return team;
};

const Schema = Yup.object().shape({
 
  name: Yup.string()
    .min(2, "Business Name too short!")
    .max(32, "Business Name too long!")
    .required("Business Name is Required!"),
  contact: Yup.string()
    .min(2, "Contact Name too short!")
    .max(32, "Contact Name too long!")
    .required("Contat Name is Required!"),
  email: Yup.string().required("Email is Required!"),
  Address: Yup.string().required("Home Address is Required!"),
  Address_ZipCode: Yup.string().required("Zip Code is Required!"),

  city: Yup.string().required("City is Required!"),
  status: Yup.string().required("Status is Required!"),
  project: Yup.string().required("Project is Required!")
});




const CustomersModal = (props) => {
  const { classes, open, customer, id, mode, reload } = props;
  const { fetchCustomer, customerCancel, customerPut, customerAdd } = props;
  let rows = []

  // if(employee) {

  //     let fetchedName = [];
  //     console.log(employee);
  //     employee.membersforEach((r) => {

  //       let team = test(r.name);
  //       let id = r.id;
  //       let data = { id, team };
  //       fetchedName.push(data);
  //     });
  //     rows = fetchedName;
  // };

  useEffect(() => {
    if (id !== null) {
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

  const CustomTeamComponent = props => {
    return (
      <Select fullWidth {...props} className={classes.input} disabled={mode === 'view' ? true : null}>
        <MenuItem value={1}>Alpha</MenuItem>
        <MenuItem value={2}>Bravo</MenuItem>
        <MenuItem value={3}>Charlie</MenuItem>
        <MenuItem value={4}>Delta</MenuItem>
        <MenuItem value={5}>Foxtrot</MenuItem>
        <MenuItem value={6}>Oscar</MenuItem>
        <MenuItem value={7}>Sierra</MenuItem>
        <MenuItem value={8}>Tango</MenuItem>
        <MenuItem value={9}>Yankee</MenuItem>
      </Select>
    );
  };

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

      {customer || mode === 'add' ? <Formik
        validationSchema={Schema}
        initialValues={{
       
          name: customer ? customer.name : "",
          contact: customer ? customer.contact : "",
          email: customer ? customer.email : "",
          phone: customer ? customer.phone : "",
          //Address_ZipCode: customer ? customer.Address_ZipCode : "",
          //city: customer ? customer.city : "",
          status: customer ? customer.status.id : "",
          //team: customer ? customer.team : ""
        }}
        onSubmit={values => {
       
          console.log('onSubmit CM', values)
          let newStatus = statuses.filter(s => values.status === s.id);
          
          values.status = newStatus[0];
          
          if (customer) {
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
                          
                        
                      </div>

                      <div className={classes.container}>
                      <InputLabel>Business Name</InputLabel>
                        {errors.name && touched.name ? (
                          <div className={classes.errorMessage}>
                            {errors.name}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="name"
                          disableUnderline={true}
                          autoComplete="off"
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
                            <InputLabel>Contact Name</InputLabel>
                        {errors.contact && touched.contact ? (
                          <div className={classes.errorMessage}>
                            {errors.contact}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="contact"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                      
                        {/* <InputLabel>Zip Code</InputLabel>
                        {errors.Address_ZipCode&& touched.Address_ZipCode ? (
                          <div className={classes.errorMessage}>
                            {errors.Address_ZipCode}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="zipCode"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        /> */}
                      </div>
                      <div className={classes.container}>
                        {/* <InputLabel>City</InputLabel>
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
                        /> */}
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
                       
                            <InputLabel>Phone</InputLabel>
                        {errors.phone && touched.phone ? (
                          <div className={classes.errorMessage}>
                            {errors.phone}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="phone"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />

                        {/* <InputLabel>Team</InputLabel>
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
                          //disableUnderline={true}
                          as={CustomTeamComponent}
                        /> */}
                   
                       <div className={classes.buttons}>
                      {mode !== 'view' ? <Button className={classes.submitButton}
                        variant="contained"
                        type="submit"
                      >
                        Submit
                          </Button> : null}
                      <Button
                        variant="contained"
                        //color={mode === 'view' ? 'primary' : 'secondary'}
                        onClick={() => customerCancel()}
                      >
                        {mode === 'view' ? 'Back' : "Cancel"}
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
    id: state.customers.selected.id,
    customer: state.customers.customer,
    mode: state.customers.selected.mode
  }
}

export default connect(mapStateToProps, {fetchCustomer, customerCancel, customerPut, customerAdd})(withStyles(styles)(CustomersModal));