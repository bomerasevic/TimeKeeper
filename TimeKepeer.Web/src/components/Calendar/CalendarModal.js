import React, { Fragment, useEffect, useState } from "react";
import {
    Container,
    Grid,
    AppBar,
    Tabs,
    Tab,
    Paper,
    Divider,
    MenuItem,
    Select,
    FormControl
} from "@material-ui/core";

import moment from "moment";
import CalendarTask from "./CalendarTask";
import CalendarAbsent from "./CalendarAbsent";

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

function CalendarModal(props) {
    const [value, setValue] = useState(props.day.dayType.id < 8 ? props.day.dayType.id : 1);

    const handleChange = event => {
        setValue(event.target.value);
    };

    useEffect(() => {
        setValue(props.day.dayType.id < 8 ? props.day.dayType.id : 1);
    }, [props.day.id, value]);
    return (
        <Fragment>
            {console.log("CALENDAR MODAL PROPS", props)}
            <Container>
                <Grid container>
                    <Grid item sm={12}>
                        <AppBar position="static">
                            <Tabs
                                value={0}
                                variant="fullWidth"
                                onChange={handleChange}
                                aria-label="Working Hours Entry"
                            >
                                <Tab
                                    label={`Day ${moment(props.day.date).format("DD/MM/YYYY")}`}
                                    {...props.a11yProps(0)}
                                />
                            </Tabs>
                        </AppBar>
                        <Paper>
                            <form>
                                <FormControl>
                                    <CustomeSelectDayTypes value={value} onChange={handleChange} />
                                </FormControl>
                            </form>
                            {value === 1 ? (
                                <div>
                                    {props.day.jobDetails.length > 0
                                        ? props.day.jobDetails.map(jobDetail => (
                                            <div>
                                                {console.log("JOB DETAIL", jobDetail)}
                                                <CalendarTask
                                                    key={jobDetail.id}
                                                    day={props.day}
                                                    data={jobDetail}
                                                    projects={props.projects}
                                                />
                                            </div>
                                        ))
                                        : null}
                                    <Divider style={{ width: "100%", margin: "1rem 0" }} />
                                    <CalendarTask day={props.day} projects={props.projects} />
                                </div>
                            ) : (
                                    <CalendarAbsent value={value} day={props.day} />
                                )}
                        </Paper>
                    </Grid>
                </Grid>
            </Container>
        </Fragment>
    );
}

export default CalendarModal;