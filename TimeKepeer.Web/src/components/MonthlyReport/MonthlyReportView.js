import React, { Fragment, useState, useEffect } from "react";
import TableView from "../TableView/TableView";
import "../red/EmployeesStyles";
import { getMonthlyReport, startLoading } from "../../store/actions/monthlyReportActions";
import { connect } from "react-redux";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Paper,
  Tooltip,
  IconButton,
  Button,
  CircularProgress,
  Backdrop,
  Toolbar,
  Typography
} from "@material-ui/core";
import NavigationLogin from "../NavigationLogin/NavigationLogin";


const MonthlyReport =(props) => {
  const [selectedYear, setSelectedYear] = useState(2019);
  const [selectedMonth, setSelectedMonth] = useState(1);
  const { loading, error, classes} = props;
  const title = "Monthly Overview";
  const backgroundImage = "/images/customers.jpg";

  useEffect(() => {
    props.getMonthlyReport(selectedYear, selectedMonth);
  }, [selectedYear, selectedMonth]);

  const handleSelectedYear = e => {
    setSelectedYear(e.target.value);
  };
  const handleSelectedMonth = e => {
    setSelectedMonth(e.target.value);
  };
  console.log("HEHHHHHHHHHHHHHHHHHEHHHHHHHHHH");

  return (
    <Fragment>
        < NavigationLogin />
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
            ) : (
        <Fragment>
          <TableView
            title={title}
            backgroundImage={backgroundImage}
            table={props.monthlyReport.table}
            selectedYear={selectedYear}
            handleSelectedYear={handleSelectedYear}
            selectedMonth={selectedMonth}
            handleSelectedMonth={handleSelectedMonth}
            hasOptions
            optionSubmit={true}
            sumTotals={true}
          />
        </Fragment>
      )}
   
    </Fragment>
  );
}

const mapStateToProps = state => {
  return {
    monthlyReport: state.monthlyReport
  };
};

export default connect(mapStateToProps, { getMonthlyReport, startLoading })(MonthlyReport);