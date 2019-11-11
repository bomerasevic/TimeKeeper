import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FilledInput from "@material-ui/core/FilledInput";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import { useFormik, Formik } from "formik";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import axios from "axios";
import config from "../../config";
function getModalStyle() {
    const top = 50;
    const left = 50;
    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`
    };
}
const useStyles = makeStyles(theme => ({
    paper: {
        position: "absolute",
        backgroundColor: theme.palette.background.paper,
        boxShadow: theme.shadows[5],
        padding: theme.spacing.unit * 4,
        outline: "none"
    },
    button: {
        margin: theme.spacing.unit
    },
    input: {
        display: "none"
    }
}));
export default function EmployeeModal(props) {
    const [statuses, setStatuses] = useState({});
    const [positions, setPosition] = useState({});
    const [employeeValues, setEmployeeValues] = useState({});
    useEffect(() => {
        if (props.edit === true) {
            console.log("props.employeeId", props.employeeId);
            axios
                .get(`${config.apiUrl}People/${props.employeeId}`, {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: config.token
                    }
                })
                .then(response => {
                    console.log("response.data", response.data);
                    setEmployeeValues(response.data);
                    console.log("employeeValues response", employeeValues);
                })
                .catch(error => {
                    console.log("The error has occured: " + error);
                });
        }
    }, []);
    useEffect(() => {
        if (statuses.values === undefined && props.edit === false) {
            axios
                .get(`${config.apiUrl}Master/employee-statuses`, {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: config.token
                    }
                })
                .then(response => {
                    setStatuses(response.data);
                })
                .catch(error => {
                    console.log("The error has occured: " + error);
                });
        }
        if (positions.values === undefined && props.edit === false) {
            axios
                .get(`${config.apiUrl}Master/employee-positions`, {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: config.token
                    }
                })
                .then(response => {
                    setPosition(response.data);
                })
                .catch(error => {
                    console.log("The error has occured: " + error);
                });
        }
    });
    const classes = useStyles();
    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            phoneNumber: "",
            birthDate: "",
            employeeBeginDate: "",
            status: "",
            position: "",
            employeeEndDate: ""
        },
        onSubmit: values => {
            axios
                .post(`${config.apiUrl}People`, values, {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: config.token
                    }
                })
                .then(response => {
                    setPosition(response.data);
                })
                .catch(error => {
                    console.log("The error has occured: " + error);
                });
        }
    });
    return (
        <Grid
            container
            direction="column"
            md={9}
            justify="center"
            alignItems="center"
            style={getModalStyle()}
            className={classes.paper}
        >
            <Formik>
                <form onSubmit={formik.handleSubmit}>
                    <Grid md={12}>
                        <h3>
                            {props.title} {props.employeeId}
                        </h3>
                    </Grid>
                    <Grid container justify="space-evenly" md={12} direction="row">
                        <Grid md={4}>
                            <input
                                accept="image/*"
                                className={classes.input}
                                id="contained-button-file"
                                multiple
                                type="file"
                            />
                            <label htmlFor="contained-button-file">
                                <Button
                                    variant="contained"
                                    component="span"
                                    className={classes.button}
                                >
                                    Upload
                                </Button>
                            </label>
                        </Grid>
                        <Grid md={4}>
                            <TextField
                                name="firstName"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={
                                    props.edit === false
                                        ? formik.values.firstName
                                        : employeeValues.firstName
                                }
                                id="first-name"
                                label={props.edit === false ? "First name" : ""}
                                className={classes.textField}
                                margin="normal"
                                variant="filled"
                            />
                            <TextField
                                name="lastName"
                                onChange={formik.handleChange}
                                value={formik.values.lastName}
                                id="last-name"
                                label="Last name"
                                className={classes.textField}
                                margin="normal"
                                variant="filled"
                            />
                            <TextField
                                name="email"
                                onChange={formik.handleChange}
                                value={formik.values.email}
                                id="email"
                                label="Email"
                                className={classes.textField}
                                type="email"
                                autoComplete="email"
                                margin="normal"
                                variant="filled"
                            />
                            <TextField
                                name="phoneNumber"
                                value={formik.values.phoneNumber}
                                onChange={formik.handleChange}
                                id="Phone number"
                                label="Phone number"
                                className={classes.textField}
                                margin="normal"
                                variant="filled"
                            />
                            <TextField
                                name="birthDate"
                                value={formik.values.birthDate}
                                onChange={formik.handleChange}
                                id="birth-date"
                                label="Birth date"
                                type="date"
                                variant="filled"
                                defaultValue={Date.now()}
                                InputLabelProps={{
                                    shrink: true
                                }}
                            />
                        </Grid>
                        <Grid md={4} container justify="space-evenly" direction="column">
                            <TextField
                                name="employeeBeginDate"
                                value={formik.values.employeeBeginDate}
                                onChange={formik.handleChange}
                                id="employee-begin-date"
                                label="Employee begin date"
                                type="date"
                                variant="filled"
                                defaultValue={Date.now()}
                                InputLabelProps={{
                                    shrink: true
                                }}
                            />
                            <FormControl variant="filled" className={classes.formControl}>
                                <InputLabel htmlFor="filled-age-simple">Status</InputLabel>
                                <Select
                                    input={
                                        <FilledInput
                                            name="status"
                                            id="status"
                                            value={formik.values.status}
                                            onChange={formik.handleChange}
                                        />
                                    }
                                >
                                    <MenuItem value={statuses[0]}>Active</MenuItem>
                                    <MenuItem value={statuses[1]}>Trial</MenuItem>
                                    <MenuItem value={statuses[2]}>Leaver</MenuItem>
                                </Select>
                            </FormControl>
                            <FormControl variant="filled" className={classes.formControl}>
                                <InputLabel htmlFor="filled-age-simple">Job title</InputLabel>
                                <Select
                                    input={
                                        <FilledInput
                                            name="position"
                                            id="title"
                                            value={formik.values.position}
                                            onChange={formik.handleChange}
                                        />
                                    }
                                >
                                    <MenuItem value={positions[0]}>DEV</MenuItem>
                                    <MenuItem value={positions[1]}>UIX</MenuItem>
                                    <MenuItem value={positions[2]}>QAE</MenuItem>
                                    <MenuItem value={positions[3]}>MGR</MenuItem>
                                    <MenuItem value={positions[4]}>HRM</MenuItem>
                                    <MenuItem value={positions[5]}>CEO</MenuItem>
                                    <MenuItem value={positions[6]}>CTO</MenuItem>
                                    <MenuItem value={positions[7]}>COO</MenuItem>
                                </Select>
                            </FormControl>
                            <TextField
                                name="employeeEndDate"
                                value={formik.values.employeeEndDate}
                                onChange={formik.handleChange}
                                id="employee-end-date"
                                label="Employee end date"
                                type="date"
                                variant="filled"
                                defaultValue={Date.now()}
                                InputLabelProps={{
                                    shrink: true
                                }}
                            />
                            <Button type="submit" variant="contained" color="primary">
                                Submit
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </Formik>
        </Grid>
    );
}
