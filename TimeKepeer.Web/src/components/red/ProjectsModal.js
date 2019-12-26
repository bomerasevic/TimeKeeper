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
import {fetchProject, projectCancel, projectPut, projectAdd} from '../../store/actions/index';



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

// fetchEmployee = id => {
//   if (id === 666) {
//     this.setState({ finish: true });
//   } else {
//     axios(`${config.apiUrl}employees/${id}`, {
//       headers: {
//         "Content-Type": "application/json",
//         Authorization: config.token
//       }
//     })
//       .then(res => {
//         //console.log(res.data.members);
//         let fetchedName = [];
//         res.data.members.forEach(r => {
//           let team = test(r.name);
//           let id = r.id;
//           let data = { id, team };
//           fetchedName.push(data);
//         });
//         console.log(fetchedName);

//         this.setState({
//           employee: res.data,
//           rows: fetchedName,
//           finish: true
//         });
//       })
//       .catch(() => this.setState({ finish: true }));
//   }
// };


const ProjectsModal = (props) => {
    const { classes, open, project, id, mode } = props;
    const { fetchProject, projectCancel, projectPut, projectAdd } = props;
    let rows = []

    // if(project) {
     
    //     let fetchedName = [];
    //     console.log(project);
    //     project.members.forEach(r => {

    //       let team = test(r.name);
    //       let id = r.id;
    //       let data = { id, team };
    //       fetchedName.push(data);
    //     });
    //     rows = fetchedName
    // }
   
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
              name: project ? project.name : "",
            
              StartDate: project
                ? moment(project.beginDate).format("YYYY-MM-DD")
                : "",
              EndDate: project
                ? moment(project.endDate).format("YYYY-MM-DD")
                : "",
             
              status: project ? project.status.id : ""
            }}
            onSubmit={values => {
             
              values.EndDate = moment(values.EndDate).format(
                "YYYY-MM-DD HH:mm:ss"
              );
              values.StartDate = moment(values.StartDate).format(
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

            
              // if (employee) {
              //   values.id = employee.id;
              //   axios
              //     .put(
              //       `${config.apiUrl}employees/${id}`,
              //       values,
              //       config.authHeader
              //     )
              //     .then(res => {
              //       handleClose();
              //     })
              //     .catch(err => {
              //       this.setState({ loading: false });
              //       console.log("error");
              //     });
              // } else {
              //   console.log(values);
              //   axios
              //     .post(`${config.apiUrl}employees`, values, config.authHeader)
              //     .then(res => {
              //       handleClose();
              //     })
              //     .catch(err => {
              //       this.setState({ loading: false });
              //       console.log("error");
              //     });
              // }
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
                        {errors.name && touched.name ? (
                          <div className={classes.errorMessage}>
                            {errors.name}
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
                        {errors.Description && touched.Description ? (
                          <div className={classes.errorMessage}>
                            {errors.Description}
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
                        {errors.StartDate &&
                        touched.StartDate ? (
                          <div className={classes.errorMessage}>
                            {errors.StartDate}
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
                        {errors.StatusId && touched.StatusId ? (
                          <div className={classes.errorMessage}>
                            {errors.StatusId}
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
                        {errors.TeamId && touched.TeamId ? (
                          <div className={classes.errorMessage}>
                            {errors.TeamId}
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
                        {errors.EndDate &&
                        touched.EndDate ? (
                          <div className={classes.errorMessage}>
                            {errors.EndDate}
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
                        {errors.CustomerdId && touched.CustomerId ? (
                          <div className={classes.errorMessage}>
                            {errors.CustomerId}
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
                        {errors.PricingId && touched.PricingId ? (
                          <div className={classes.errorMessage}>
                            {errors.PricingId}
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
                        {errors.Amount && touched.Amount ? (
                          <div className={classes.errorMessage}>
                            {errors.Amount}
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
                            //color="primary"
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