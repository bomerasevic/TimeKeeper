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
import {fetchProject, projectCancel, projectPut, projectAdd, employeeDelete} from '../../store/actions/index';



const statuses = [
  { id: 1, name: "In progress" },
  { id: 2, name: "On hold" },
  { id: 3, name: "Finished" },
  { id: 4, name: "Canceled" }
];

const team = membersData => {
	let index = membersData.indexOf(",");
	let team = membersData.substr(index + 1);
  
	return team;
  };

const Schema = Yup.object().shape({
  projectName: Yup.string()
    .min(2, "Project Name too short!")
    .max(32, "Project Name too long!")
    .required("Project Name is Required!"),
    description: Yup.string()
    .max(250, "Description is too long!"),
 startDate: Yup.string().required(
    "Project Begin Date is Required!"
  ),
  endDate: Yup.string().required(
    "Project End Date is Required!"
  ),
  status: Yup.string().required("Status is Required!")

});




const ProjectsModal = (props) => {
    const { classes, open, project, id, mode } = props;
    const { fetchEmployee, employeeCancel, employeePut, employeeAdd } = props;
    let rows = []

    if(project) {
     
        let fetchedName = [];
        console.log(project);
        project.members.forEach(r => {

          let team = test(r.name);
          let id = r.id;
          let data = { id, team };
          fetchedName.push(data);
        });
        rows = fetchedName
    }
   
    useEffect(() => {
      if(id !== null) {
        fetchProject(id)
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
          <MenuItem value={1}>In progress</MenuItem>
          <MenuItem value={2}>On hold</MenuItem>
          <MenuItem value={3}>Finsihed</MenuItem>
          <MenuItem value={4}>Cancel</MenuItem>
        </Select>
      );
    };

    return (
      <React.Fragment>
       
          {project || mode==='add' ? <Formik
            validationSchema={Schema}
            initialValues={{
              projectName: project ? project.projectName : "",
            
              startDate: project
                ? moment(project.beginDate).format("YYYY-MM-DD")
                : "",
              endDate: project
                ? moment(project.endDate).format("YYYY-MM-DD")
                : "",
             
              status: project ? project.status.id : ""
            }}
            onSubmit={values => {
             
              values.endDate = moment(values.endDate).format(
                "YYYY-MM-DD HH:mm:ss"
              );
              values.beginDate = moment(values.beginDate).format(
                "YYYY-MM-DD HH:mm:ss"
              );

           
              let newStatus = statuses.filter(s => values.status === s.id);

              values.status = newStatus[0];

              if(project) {
                values.id = project.id
                projectPut(project.id, values)
              } else {
                values.image = 'image'
                projectAdd(values)
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
                        <InputLabel>Project Name</InputLabel>
                        {errors.projectName && touched.projectName ? (
                          <div className={classes.errorMessage}>
                            {errors.projectName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="projectName"
                          autoComplete="off"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                        <InputLabel>Description</InputLabel>
                        {errors.description && touched.description ? (
                          <div className={classes.errorMessage}>
                            {errors.description}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="description"
                          disableUnderline={true}
                          as={CustomInputComponent}
                        />
                         </div>
                         <div className={classes.container}>
                        
                        <InputLabel> Start Date</InputLabel>
                        {errors.startDate &&
                        touched.startDate ? (
                          <div className={classes.errorMessage}>
                            {errors.startDate}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="startDate"
                          autoComplete="off"
                          disableUnderline={true}
                          type="date"
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
                        <InputLabel> End Date</InputLabel>
                        {errors.endDate &&
                        touched.endDate ? (
                          <div className={classes.errorMessage}>
                            {errors.endDate}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="endDate"
                          disableUnderline={true}
                          type="date"
                          as={CustomInputComponent}
                        />
                        </div>
                       <div className={classes.container}>
                       <InputLabel>Customer</InputLabel>
                        {errors.customer && touched.customer ? (
                          <div className={classes.errorMessage}>
                            {errors.team}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="customer"
                          disableUnderline={true}
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Pricing</InputLabel>
                        {errors.pricing && touched.pricing ? (
                          <div className={classes.errorMessage}>
                            {errors.team}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="pricing"
                          disableUnderline={true}
                          type="number"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Amount (fixed bid only)</InputLabel>
                        {errors.amount && touched.amount ? (
                          <div className={classes.errorMessage}>
                            {errors.amount}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="amount"
                          disableUnderline={true}
                          type="number"
                          autoComplete="off"
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
                            onClick={() => projectCancel()}
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
    id: state.projects.selected.id,
    employee: state.projects.project,
    mode: state.projects.selected.mode
  }
}

export default connect(mapStateToProps, {fetchProject, projectCancel, projectPut, projectAdd})(withStyles(styles)(ProjectsModal));