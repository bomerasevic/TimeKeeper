import React, { Fragment, useState, useEffect } from "react";
//import BlockElementSpinner from "../../components/BlockElementSpinner/BlockElementSpinner";
import { connect } from "react-redux";
// import data from "./data";
import { withStyles } from "@material-ui/core/styles";
import styles from "../AnnualReport/AnnualReportStyles";
import PieChart from "../../components/Charts/PieChart";
import BarChart from "../../components/Charts/BarChart";
import { Container, Grid, Paper, TextField, MenuItem , Backdrop, CircularProgress, Button} from "@material-ui/core";
import { getCompanyDashboard } from "../../store/actions/companyDashboardActions";
//import "./CompanyDashboard.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";



function CompanyDashboard({ data, isLoading, getCompanyDashboard, classes, error }) {
    const [selectedYear, setSelectedYear] = useState(2019);
    const [selectedMonth, setSelectedMonth] = useState(1);
   

    useEffect(() => {
        getCompanyDashboard(selectedYear, selectedMonth);
    }, [selectedYear, selectedMonth]);

    const handleSelectedYear = e => {
        setSelectedYear(e.target.value);
    };
    const handleSelectedMonth = e => {
        setSelectedMonth(e.target.value);
    };

    const TotalHoursChart = ({ data }) => {
        console.log(data);
        const modifiedData = [
            { x: "Working Hours", y: parseInt(data.totalWorkingHours) },
            { x: "Remaining Hours", y: parseInt(data.totalHours - data.totalWorkingHours) }
        ];
        return <PieChart padAngle={1} data={modifiedData} title={"Total Hours"} />;
    };
    const RevenueChart = ({ data }) => {
        const modifiedData = data.map((x, i) => {
            return { name: x.project.name, value: parseInt(x.revenue) };
        });
        return (
            <Fragment>
                <BarChart
                    data={modifiedData}
                    height={"auto"}
                    width={350}
                    xLabel="Projects"
                    yLabel="Revenue"
                />
                <h6 className="text-center">Revenue</h6>
            </Fragment>
        );
    };
    const PtoChart = ({ data }) => {
        const modifiedData = data.map((x, i) => {
            return { name: x.team.name, value: parseInt(x.paidTimeOff) };
        });
        return (
            <Fragment>
                <BarChart
                    data={modifiedData}
                    height={"auto"}
                    width={350}
                    xLabel="Teams"
                    yLabel="Paid Time Off"
                />
                <h6 className="text-center">Paid Time Off</h6>
            </Fragment>
        );
    };
    const OvertimeChart = ({ data }) => {
        const modifiedData = data.map((x, i) => {
            return { name: x.team.name, value: parseInt(x.overtime) };
        });
        return (
            <Fragment>
                <BarChart
                    data={modifiedData}
                    height={"auto"}
                    width={350}
                    xLabel="Teams"
                    yLabel="Overtime"
                />
                <h6 className="text-center">Overtime</h6>
            </Fragment>
        );
    };
    const MissingEntriesChart = ({ data }) => {
        const modifiedData = data.map(x => {
            return { name: x.team.name, value: parseInt(x.missingEntries) };
        });
        return (
            <Fragment>
                <BarChart
                    data={modifiedData}
                    height={"auto"}
                    width={500}
                    horizontal={true}
                    angle={0}
                    labelPadding={0}
                    fontSize={7}
                />
                <h6 className="text-center">Missing Entries</h6>
            </Fragment>
        );
    };
    const UtilizationCharts = ({ data }) => {
        return data.map((x, i) => {
            const modifiedData = [
                { x: "Working Hours", y: parseInt(x.workingHours) },
                { x: "Remaining Hours", y: parseInt(x.totalHours - x.workingHours) }
            ];
            return (
                <Grid item md={3} key={i}>
                    <PieChart height={250} padAngle={1} data={modifiedData} title={x.role.name} />
                </Grid>
            );
        });
    };
    const YearDropdown = () => (
        <TextField
            variant="outlined"
            id="selected-year"
            select
            label="Selected Year"
            value={selectedYear}
            onChange={handleSelectedYear}
            margin="normal"
            fullWidth={true}
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
            id="selected-month"
            z
            select
            label="Selected Month"
            value={selectedMonth}
            onChange={handleSelectedMonth}
            margin="normal"
            fullWidth={true}
        >
            {[
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            ].map((x, i) => {
                return (
                    <MenuItem value={i + 1} key={x}>
                        {x}
                    </MenuItem>
                );
            })}
        </TextField>
    );

    return (
        <Fragment>

            < NavigationLogin />
            {isLoading ? (
                <Backdrop open={isLoading}>
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
            ) : (
                <Container className="mt-3 mb-5">
                    <Grid
                        container
                        spacing={4}
                        alignItems="center"
                        justify="space-between"
                        className="mb-1-5"
                    >
                        <Grid item sm={8}>
                            <h3 className="mb-0 mt-0">Company Dashboard</h3>
                        </Grid>
                        <Grid item sm={2}>
                            <MonthDropdown />
                        </Grid>
                        <Grid item sm={2}>
                            <YearDropdown />
                        </Grid>
                    </Grid>
                    <Grid
                        container
                        spacing={4}
                        justify="space-between"
                        alignItems="center"
                        className="mb-1-5"
                    >
                        <Grid item md={4}>
                            <Paper elevation={3} className="company-totals">
                                <div className="flex flex-column align-items-center justify-content-center">
                                    <h4>TOTAL HOURS</h4> <h1 className="mb-0 mt-0">{data.totalHours}</h1>
                                </div>
                            </Paper>
                        </Grid>
                        <Grid item md={4}>
                            <Paper elevation={3} className="company-totals">
                                <div className="flex flex-column align-items-center justify-content-center">
                                    <h4>EMPLOYEES</h4> <h1 className="mb-0 mt-0">{data.employeesCount}</h1>
                                </div>
                            </Paper>
                        </Grid>
                        <Grid item md={4}>
                            <Paper elevation={3} className="company-totals">
                                <div className="flex flex-column align-items-center justify-content-center">
                                    <h4>PROJECTS </h4> <h1 className="mb-0 mt-0">{data.projectsCount}</h1>
                                </div>
                            </Paper>
                        </Grid>
                    </Grid>
                    <Paper elevation={3} className="company-totals" className="mb-3">
                        <Grid container spacing={4} justify="center" alignItems="center">
                            <Grid item md={3}>
                                <TotalHoursChart data={data} />
                            </Grid>
                            <Grid item md={3}>
                                <RevenueChart data={data.projects} />
                            </Grid>
                            <Grid item md={3}>
                                <PtoChart data={data.teams} />
                            </Grid>
                            <Grid item md={3}>
                                <OvertimeChart data={data.teams} />
                            </Grid>
                        </Grid>
                    </Paper>
                    <Paper elevation={3} className="company-totals">
                        <Grid container spacing={4} justify="center" alignItems="center">
                            <Grid item md={3}>
                                <MissingEntriesChart data={data.teams} />
                            </Grid>
                            <UtilizationCharts data={data.roles} />
                        </Grid>
                    </Paper>
                </Container>
            )}
            {isLoading}
        </Fragment>
    );
}

const mapStateToProps = state => {
    return {
        data: state.companyDashboard.data,
        isLoading: state.companyDashboard.isLoading
    };
};

export default connect(mapStateToProps, { getCompanyDashboard })(
    withStyles(styles)(CompanyDashboard)
);
