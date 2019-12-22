import React, { Fragment, useState, useEffect } from "react";

import TableView from "../../components/TableView/TableView";
import { MenuItem, TextField } from "@material-ui/core";
import { getMonthlyReport, startLoading } from "../../store/actions/monthlyReportActions";
import { connect } from "react-redux";

import NavigationLogin from "../NavigationLogin/NavigationLogin";

function MonthlyReport(props) {
    const [selectedYear, setSelectedYear] = useState(2019);
    const [selectedMonth, setSelectedMonth] = useState(1);
    const title = "Monthly Overview";
    const backgroundImage = "/images/customers.jpg";

    useEffect(() => {
        props.getMonthlyReport(selectedYear, selectedMonth);
    }, [selectedYear, selectedMonth]);

    const YearDropdown = () => (
        <TextField
            variant="outlined"
            id="Selected Year"
            select
            label="Selected Year"
            value={selectedYear}
            onChange={e => {
                props.startLoading();
                setSelectedYear(e.target.value);
            }}
            margin="normal"
        >
            {[2019, 2018, 2017].map(x => {
                return (
                    <MenuItem value={x} key={x}>
                        {x}
                    </MenuItem>
                );
            })}
        </TextField>
    );

    const MonthDropdown = () => (
        <TextField
            variant="outlined"
            id="Selected Month"
            select
            label="Selected Month"
            value={selectedMonth}
            onChange={e => {
                props.startLoading();
                setSelectedMonth(e.target.value);
            }}
            margin="normal"
        >
            {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map(x => {
                return (
                    <MenuItem value={x} key={x}>
                        {x}
                    </MenuItem>
                );
            })}
        </TextField>
    );
    return (
        <Fragment>
            < NavigationLogin />
            {!props.monthlyReport.isLoading && (
                <Fragment>
                    <YearDropdown />
                    <MonthDropdown />
                    <TableView
                        title={title}
                        backgroundImage={backgroundImage}
                        table={props.monthlyReport.table}
                    />
                </Fragment>
            )}
            {props.monthlyReport.isLoading}
        </Fragment>
    );
}

const mapStateToProps = state => {
    return {
        monthlyReport: state.monthlyReport
    };
};

export default connect(mapStateToProps, { getMonthlyReport, startLoading })(MonthlyReport);