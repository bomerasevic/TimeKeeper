import React, { useState, useEffect, Fragment } from "react";

import { apiGetAllRequest } from "../../utils/api";
import { withStyles } from "@material-ui/core/styles";
import styles from "../AnnualReport/AnnualReportStyles";
import Calendar from "react-calendar";
import moment from "moment";
import { loadCalendar, rldCal } from "../../store/actions/calendarActions";
import {  Backdrop, CircularProgress, Button } from "@material-ui/core";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import "./Calendar.css"
import CalendarModal from "./CalendarModal";
import NavigationLogin from "../NavigationLogin/NavigationLogin";

function CalendarDisplay(props) {
    const {classes, error, loading} = props;
    const [date, setDate] = useState(new Date(2019, 5, 6, 10, 33, 30, 0));
    const [year, setYear] = useState(moment(date).format("YYYY"));
    const [month, setMonth] = useState(moment(date).format("MM"));
    const [day, setDay] = useState(moment(date).format("DD"));
    const [employeeId] = useState(props.user.user.user.id);
    const [projects, setProjects] = useState([]);
    const [selectedTab, setSelectedTab] = useState(0);

    useEffect(() => {
        apiGetAllRequest("http://192.168.60.72/timekeeper/api/projects").then(res => {
            setProjects(res.data.data);
        });

        props.loadCalendar(employeeId, year, month);
        if (props.calendarMonth) {
            const selectedYear = moment(date).format("YYYY");
            const selectedMonth = moment(date).format("MM");
            const selectedDay = moment(date).format("DD");
            setDate(date);
            setYear(selectedYear);
            setMonth(selectedMonth);
            setDay(selectedDay);
        }
        props.rldCal(false);
    }, [props.reload]);
    const handleSelectedTab = (event, newValue) => {
        setSelectedTab(newValue);
    };
    const changeData = selectedDate => {
        const selectedYear = moment(selectedDate).format("YYYY");
        const selectedMonth = moment(selectedDate).format("MM");
        const selectedDay = moment(selectedDate).format("DD");
        if (selectedYear !== year || selectedMonth !== month) {
            props.loadCalendar(employeeId, selectedYear, selectedMonth);
            setDate(selectedDate);
            setYear(selectedYear);
            setMonth(selectedMonth);
            setDay(selectedDay);
        } else if (selectedDay !== day) {
            setDate(selectedDate);
            setDay(selectedDay);
        }
    };
    function onChange(date) {
        changeData(date);
    }

    function a11yProps(index) {
        return {
            id: `tab-${index}`,
            "aria-controls": `tabpanel-${index}`
        };
    }

    return (
        <div>

            < NavigationLogin />
            <Fragment>
            {loading ? (
                            <Backdrop open={loading}>
                                <div className={classes.center}>
                                    <CircularProgress size={100} className={classes.loader} />
                                    <h1 className={classes.loaderText}>Loading...</h1>
                                </div>
                            </Backdrop>
                        ) : error ? (
                            <Backdrop open={true}>
                                <div className={classes.center}>
                                    <h1 className={classes.loaderText}>{error.message}</h1>
                                    <h2 className={classes.loaderText}>Please reload the application</h2>
                                    <Button variant="outlined" size="large" className={classes.loaderText}>
                                        Reload
                                    </Button>
                                </div>
                            </Backdrop>
                    
                        ) : null  })    
                    
            </Fragment>

            <Calendar style={{zIndex: "1"}} onChange={onChange} value={date} />
            <div>
                {props.calendarMonth &&
                    moment(props.calendarMonth[day - 1].date).format("YYYY-MM-DD") ===
                    moment(date).format("YYYY-MM-DD") ? (
                        <div>
                            <CalendarModal
                                selectedTab={selectedTab}
                                handleSelectedTab={handleSelectedTab}
                                a11yProps={a11yProps}
                                calendarMonth={props.calendarMonth}
                                projects={projects}
                                day={props.calendarMonth[day - 1]}
                            />
                        </div>
                    ) : (
                        <h2> No data</h2>
                        

                        )}
                        
                   
            </div>
        </div>
    );
}

const mapStateToProps = state => {
    return {
        calendarMonth: state.calendarMonth.data.data,
        user: state.user,
        reload: state.calendarMonth.reload,
        loading: state.calendarMonth.loading
    };
};

export default connect(mapStateToProps, { loadCalendar, rldCal })(withRouter(withStyles(styles)(CalendarDisplay)));