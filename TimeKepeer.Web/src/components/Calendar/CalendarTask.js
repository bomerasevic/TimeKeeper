import React, { Fragment } from "react";
import { Grid, Select, MenuItem, TextField, Input, IconButton } from "@material-ui/core";
import { Formik, Form, Field } from "formik";

import AddIcon from "@material-ui/icons/Add";
import DeleteIcon from "@material-ui/icons/Delete";
import TodayIcon from "@material-ui/icons/Today";
import EditIcon from "@material-ui/icons/Edit";

import { editTask, addTask, addDayWithTask, loadCalendar, deleteTask } from "../../store/actions";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";

import moment from "moment";

const CustomSelectComponent = props => {
    return (
        <Select fullWidth {...props}>
            {props.data.map(p => (
                <MenuItem value={p.id} key={p.id}>
                    {p.name}
                </MenuItem>
            ))}
        </Select>
    );
};

const CustomeTextFieldComponent = props => <TextField {...props} />;

const CustomInputComponent = props => <Input {...props} fullWidth />;

function CalendarTask(props) {
    return (
        <Formik
            enableReinitialize
            onSubmit={(values, { resetForm }) => {
                let data = {
                    day: {
                        id: props.day.id
                    },
                    project: {
                        id: values.project
                    },
                    description: values.description,
                    hours: values.hours
                };
                if (props.day.id === 0) {
                    console.log("1");
                    const day = {
                        dayType: {
                            id: 1
                        },
                        employee: {
                            id: props.user.user.id
                        },
                        date: props.day.date
                    };
                    props.addDayWithTask(day, data);
                    resetForm();
                } else if (!props.data) {
                    props.addTask(data);
                    resetForm();
                } else {
                    data.id = props.data.id;
                    props.editTask(props.data.id, data);
                }
            }}
            initialValues={{
                hours: props.data ? props.data.hours : "",
                project: props.data ? props.data.project.id : 1,
                description: props.data ? props.data.description : ""
            }}
        >
            <Form>
                <Grid container alignItems="center" className="mb-1-5">
                    <span>{moment(props.day.date).format("YYYY-MM-DD")}</span>
                    <TodayIcon />
                </Grid>
                {console.log("PROPPOVI OPOVI", props)}
                <Fragment>
                    <Grid container spacing={4} alignItems="center">
                        <Grid item xs={3}>
                            <Grid>
                                <Field
                                    name={"project"}
                                    id={"project-select-"}
                                    label="Project"
                                    data={props.projects}
                                    as={CustomSelectComponent}
                                />
                            </Grid>
                            <Grid
                                item
                                style={{
                                    padding: "1rem 0"
                                }}
                            >
                                <Field
                                    as={CustomInputComponent}
                                    name={"hours"}
                                    placeholder="Hours Worked"
                                />
                            </Grid>
                        </Grid>
                        <Grid item xs={8}>
                            <Field
                                as={CustomeTextFieldComponent}
                                name={"description"}
                                label="Description"
                                multiline={true}
                                rows={2}
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={1} className="text-right">
                            <Grid>
                                <IconButton
                                    className="align-adjust-margin"
                                    aria-label="delete-working-hours"
                                    color="primary"
                                    type="submit"
                                >
                                    {props.data ? <EditIcon /> : <AddIcon />}
                                </IconButton>
                                {props.data ? (
                                    <IconButton
                                        aria-label="del"
                                        color="secondary"
                                        onClick={() => props.deleteTask(props.data.id)}
                                    >
                                        <DeleteIcon />
                                    </IconButton>
                                ) : null}
                            </Grid>
                        </Grid>
                    </Grid>
                </Fragment>
            </Form>
        </Formik>
    );
}

const mapStateToProps = state => {
    return {
        user: state.user,
        calendarMonth: state.calendarMonth.data.data
    };
};

export default connect(mapStateToProps, {
    editTask,
    addTask,
    addDayWithTask,
    deleteTask,
    loadCalendar
})(withRouter(CalendarTask));