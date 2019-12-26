import React, { useEffect } from "react";
import { Grid, Select, MenuItem, TextField, Input, IconButton } from "@material-ui/core";
import { Formik, Form, Field } from "formik";
import AddIcon from "@material-ui/icons/Add";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";

import { addDay } from "../../store/actions/calendarActions";

const CustomeSelectDayTypes = props => {
    return (
        <Select fullWidth {...props}>
            <MenuItem value={1}>Working</MenuItem>
            <MenuItem value={2}>Holiday</MenuItem>
            <MenuItem value={3}>Busines</MenuItem>
            <MenuItem value={4}>Religious</MenuItem>
            <MenuItem value={5}>Sick</MenuItem>
            <MenuItem value={6}>Vacation</MenuItem>
            <MenuItem value={7}>Other</MenuItem>
        </Select>
    );
};
function CalendarAbsent(props) {
    useEffect(() => { }, [props.value, props.day]);
    return (
        <div>
            <Formik
                enableReinitialize
                initialValues={{
                    dayType: props.value
                }}
                onSubmit={values => {
                    console.log("ON SUBMIT", props.value, props.user.id, props.day.date);
                    const data = {
                        dayType: {
                            id: props.value
                        },
                        employee: {
                            id: props.user.id
                        },
                        date: props.day.date
                    };
                    console.log("ADD DAY DATA", data);
                    props.addDay(data);
                }}
            >
                <Form>
                    {/* <Field name="dayType" as={CustomeSelectDayTypes}></Field> */}
                    <IconButton color="primary" type="submit">
                        <AddIcon />
                    </IconButton>
                </Form>
            </Formik>
        </div>
    );
}

const mapStateToProps = state => {
    return {
        user: state.user.user
    };
};

export default connect(mapStateToProps, { addDay })(withRouter(CalendarAbsent));