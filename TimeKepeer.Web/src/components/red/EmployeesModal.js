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
import { fetchEmployee, employeeCancel, employeePut, employeeAdd, employeeDelete } from '../../store/actions/index';


const positions = [
  { id: 1, name: "Chief Executive Officer" },
  { id: 2, name: "Chief Technical Officer" },
  { id: 3, name: "Chief Operations Officer" },
  { id: 4, name: "Manager" },
  { id: 5, name: "HR Manager" },
  { id: 6, name: "Developer" },
  { id: 7, name: "UI/UX Designer" },
  { id: 8, name: "QA Enginee" }
];

const statuses = [
  { id: 1, name: "Waiting for the task" },
  { id: 2, name: "Active" },
  { id: 3, name: "On hold" },
  { id: 4, name: "Leaver" }
];

const test = (membersData) => {
  let index = membersData.indexOf(",");
  let team = membersData.substr(index + 1);

  return team;
};

const Schema = Yup.object().shape({
  salary: Yup.number("Salary field can contain only numeric data!"),
  firstName: Yup.string()
    .min(2, "First Name too short!")
    .max(32, "First Name too long!")
    .required("First Name is Required!"),
  lastName: Yup.string()
    .min(2, "Last Name too short!")
    .max(32, "Last Name too long!")
    .required("Last Name is Required!"),
  email: Yup.string().required("Email is Required!"),
  phone: Yup.string().required("Phone Number is Required!"),
  birthday: Yup.string().required("Birth Date is Required!"),
  employmentBeginDate: Yup.string().required(
    "Employment Begin Date is Required!"
  ),
  employmentEndDate: Yup.string().required(
    "Employment End Date is Required!"
  ),
  position: Yup.string().required("Job Title is Required!"),
  status: Yup.string().required("Status is Required!")
});




const EmployeesModal = (props) => {
  const { classes, open, employee, id, mode, reload } = props;
  const { fetchEmployee, employeeCancel, employeePut, employeeAdd } = props;
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
      fetchEmployee(id)
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

  const CustomSelectComponent = props => {
    return (
      <Select fullWidth {...props} className={classes.input} disabled={mode === 'view' ? true : null}>
        <MenuItem value={1}>Chief Executive Officer</MenuItem>
        <MenuItem value={2}>Chief Technical Officer</MenuItem>
        <MenuItem value={3}>Chief Operations Officer</MenuItem>
        <MenuItem value={4}>Manager</MenuItem>
        <MenuItem value={5}>HR Manager</MenuItem>
        <MenuItem value={6}>Developer</MenuItem>
        <MenuItem value={7}>UI/UX Designer</MenuItem>
        <MenuItem value={8}>QA Engineer</MenuItem>
      </Select>
    );
  };

  const CustomStatusComponent = props => {
    return (
      <Select fullWidth {...props} className={classes.input} disabled={mode === 'view' ? true : null}>
        <MenuItem value={1}>Waiting for the task</MenuItem>
        <MenuItem value={2}>Active</MenuItem>
        <MenuItem value={3}>On hold</MenuItem>
        <MenuItem value={4}>Leaver</MenuItem>
      </Select>
    );
  };

  return (
    <React.Fragment>

      {employee || mode === 'add' ? <Formik
        validationSchema={Schema}
        initialValues={{
          salary: employee ? employee.salary : "",
          firstName: employee ? employee.firstName : "",
          lastName: employee ? employee.lastName : "",
          email: employee ? employee.email : "",
          phone: employee ? employee.phone : "",
          birthday: employee
            ? moment(employee.birthday).format("YYYY-MM-DD")
            : "",
          employmentBeginDate: employee
            ? moment(employee.beginDate).format("YYYY-MM-DD")
            : "",
          employmentEndDate: employee
            ? moment(employee.endDate).format("YYYY-MM-DD")
            : "",
          position: employee ? employee.position.id : "",
          status: employee ? employee.status.id : ""
        }}
        onSubmit={values => {
          values.birthday = moment(values.birthday).format(
            "YYYY-MM-DD HH:mm:ss"
          );
          values.endDate = moment(values.endDate).format(
            "YYYY-MM-DD HH:mm:ss"
          );
          values.beginDate = moment(values.beginDate).format(
            "YYYY-MM-DD HH:mm:ss"
          );

          let newPosition = positions.filter(p => values.position === p.id);
          let newStatus = statuses.filter(s => values.status === s.id);

          values.position = newPosition[0];
          values.status = newStatus[0];

          if (employee) {
            values.id = employee.id
            employeePut(employee.id, values)
          } else {
            values.image = 'image'
            employeeAdd(values)
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
                      className={classes.img}
                    />
                    <InputLabel>Salary</InputLabel>
                    {errors.salary && touched.salary ? (
                      <div className={classes.errorMessage}>
                        {errors.salary}
                      </div>
                    ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      name="salary"
                      type="number"
                      disableUnderline={true}
                      autoComplete="off"
                      as={CustomInputComponent}
                    />

                    <Paper className={classes.root}>
                      <Table className={classes.table}>
                        <TableHead className={classes.tableHead}>
                          <TableRow>
                            <TableCell className={classes.tableCell}>
                              Team
                                </TableCell>
                            <TableCell
                              className={classes.tableCell}
                              align="right"
                            >
                              Role
                                </TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                          {rows.map(row => (
                            <TableRow key={row.id}>
                              <TableCell component="th" scope="row">
                                {row.team}
                              </TableCell>
                              <TableCell align="right">
                                {employee.position.name}
                              </TableCell>
                            </TableRow>
                          ))}
                        </TableBody>
                      </Table>
                    </Paper>
                  </div>

                  <div className={classes.container}>
                    <InputLabel>First Name</InputLabel>
                    {errors.firstName && touched.firstName ? (
                      <div className={classes.errorMessage}>
                        {errors.firstName}
                      </div>
                    ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      name="firstName"
                      autoComplete="off"
                      disableUnderline={true}
                      as={CustomInputComponent}
                    />
                    <InputLabel>Last Name</InputLabel>
                    {errors.lastName && touched.lastName ? (
                      <div className={classes.errorMessage}>
                        {errors.lastName}
                      </div>
                    ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      autoComplete="off"
                      disableUnderline={true}
                      name="lastName"
                      as={CustomInputComponent}
                    />
                    <InputLabel>E-Mail</InputLabel>
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
                      autoComplete="off"
                      disableUnderline={true}
                      as={CustomInputComponent}
                    />
                    <InputLabel>Phone Number</InputLabel>
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
                    <InputLabel>Birth Date</InputLabel>
                    {errors.birthday && touched.birthday ? (
                      <div className={classes.errorMessage}>
                        {errors.birthday}
                      </div>
                    ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      autoComplete="off"
                      type="date"
                      name="birthday"
                      disableUnderline={true}
                      as={CustomInputComponent}
                    />
                  </div>
                  <div className={classes.container}>
                    <InputLabel>Employment Begin Date</InputLabel>
                    {errors.employmentBeginDate &&
                      touched.employmentBeginDate ? (
                        <div className={classes.errorMessage}>
                          {errors.employmentBeginDate}
                        </div>
                      ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      name="employmentBeginDate"
                      autoComplete="off"
                      type="date"
                      disableUnderline={true}
                      as={CustomInputComponent}
                    />
                    <InputLabel className={classes.statusInput}>Status</InputLabel>
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
                    <InputLabel className={classes.statusInput} >Job Title</InputLabel>
                    {errors.position && touched.position ? (
                      <div className={classes.errorMessage}>
                        {errors.position}
                      </div>
                    ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field className={classes.titleInput}
                      name="position"
                      autoComplete="off"

                      as={CustomSelectComponent}
                    />
                    <InputLabel className={classes.statusInput}>Employement End Date</InputLabel>
                    {errors.employementEndDate &&
                      touched.employementEndDate ? (
                        <div className={classes.errorMessage}>
                          {errors.employementEndDate}
                        </div>
                      ) : (
                        <div className={classes.errorMessage}> &nbsp; </div>
                      )}
                    <Field
                      autoComplete="off"
                      name="employmentEndDate"
                      disableUnderline={true}
                      type="date"
                      as={CustomInputComponent}
                    />
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
                        onClick={() => employeeCancel()}
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
    id: state.employees.selected.id,
    employee: state.employees.employee,
    mode: state.employees.selected.mode
  }
}

export default connect(mapStateToProps, { fetchEmployee, employeeCancel, employeePut, employeeAdd })(withStyles(styles)(EmployeesModal));