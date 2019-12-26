import React, { Fragment, useState, useEffect } from "react";
import TableView from "../TableView/TableView";
import "../red/EmployeesStyles";
import styles from "./MonthlyReportStyles";
import { withStyles } from "@material-ui/core/styles";
import { MenuItem, TextField, InputLabel } from "@material-ui/core";
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
  const { isLoading, error, classes} = props;
  const title = "Monthly Overview";
  const backgroundImage = "../../assets/images/overview.png";
 

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

  const YearDropdown = () => (
    <TextField 
    style={{ marginLeft: "1200px" }}
      variant="outlined"
      id="Selected Year"
      select
      label="Selected Year"
      value={selectedYear}
      onChange={(e) => {
        props.startLoading();
        setSelectedYear(e.target.value);
      }}
      margin="normal"
    
  
    >
      {[2019, 2018, 2017].map((x) => {
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
    style={{ marginLeft: "1200px"  }}
      variant="outlined"
      id="Selected Month"
      select
      label="Selected Month"
      value={selectedMonth}
      onChange={(e) => {
        
        props.startLoading();
        setSelectedMonth(e.target.value);
      }}
      margin="normal"
    >
      {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map((x) => {
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
        <Fragment>
          <MonthDropdown />
          <YearDropdown />
          <TableView
            title={title}
            
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
    monthlyReport: state.monthlyReport, 
    isLoading: state.monthlyReport.isLoading
  };
};

export default connect(mapStateToProps, { getMonthlyReport, startLoading })( withStyles(styles)(MonthlyReport)
);