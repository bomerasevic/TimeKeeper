import React, { useState, useEffect, Fragment } from "react";

import { apiGetAllRequest } from "../../utils/api";

import Calendar from "react-calendar";
import moment from "moment";
import { loadCalendar, rldCal } from "../../store/actions/calendarActions";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import "./Calendar.css"
import CalendarModal from "./CalendarModal";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import loader from "../../assets/images/loader.gif"
import Config from "../../config"
function CalendarDisplay(props) {
    const [date, setDate] = useState(new Date(2019, 5, 6, 10, 33, 30, 0));
    const [year, setYear] = useState(moment(date).format("YYYY"));
    const [month, setMonth] = useState(moment(date).format("MM"));
    const [day, setDay] = useState(moment(date).format("DD"));
    const [employeeId] = useState(props.user.user.user.id);
    const [projects, setProjects] = useState([]);
    const [selectedTab, setSelectedTab] = useState(0);

    useEffect(() => {
        apiGetAllRequest(Config.url + "api/projects").then(res => {
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
        <div >

            < NavigationLogin />
            <div style={{
                display: "flex"
            }}>
                <Calendar onChange={onChange} value={date} />
                <div style={{
                    marginTop: "100px",
                    width: "900px"
                }}>
                    {props.calendarMonth &&
                        moment(props.calendarMonth[day - 1].date).format("YYYY-MM-DD") ===
                        moment(date).format("YYYY-MM-DD") ? (
                            <div>
                                <CalendarModal
                                    style={{

                                    }}
                                    selectedTab={selectedTab}
                                    handleSelectedTab={handleSelectedTab}
                                    a11yProps={a11yProps}
                                    calendarMonth={props.calendarMonth}
                                    projects={projects}
                                    day={props.calendarMonth[day - 1]}
                                />
                            </div>
                        ) : (
                            <div>
                                <img style={{
                                    width: "100px",
                                    marginLeft: "400px"

                                }} src={loader} alt="loading..." />
                                <p>Loading data...</p>
                            </div>
                        )}
                </div>
            </div>
        </div>
    );
}

const mapStateToProps = state => {
    return {
        calendarMonth: state.calendarMonth.data.data,
        user: state.user,
        reload: state.calendarMonth.reload
    };
};

export default connect(mapStateToProps, { loadCalendar, rldCal })(withRouter(CalendarDisplay));