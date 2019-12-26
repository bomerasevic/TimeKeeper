import React, { Fragment, useState, useEffect } from "react";
import DropDownYear from "../../components/TeamTimeTracking/DropDownYear";
import styles from "./AnnualReportStyles";
import { withStyles } from "@material-ui/core/styles";
import TableView from "../../components/TableView/TableView";
import { MenuItem, TextField, InputLabel } from "@material-ui/core";
import { getAnnualReport, startLoading } from "../../store/actions/annualReportActions";
import { connect } from "react-redux";
import {

  Button,
  CircularProgress,
  Backdrop,

} from "@material-ui/core";

import NavigationLogin from "../NavigationLogin/NavigationLogin";

const AnnualReport =(props) => {
  const [selectedYear, setSelectedYear] = useState(2019);
  const title = "Annual Overview";
  const { loading, error, classes, reload} = props;
  const {isLoading} = props;
  const backgroundImage = "/images/customers.jpg";

  useEffect(() => {
    props.getAnnualReport(selectedYear);
  }, [selectedYear]);

  const YearDropdown = () => (
    
    <TextField
     style={{marginLeft:"1200px"}}
      variant = "outlined"
    
      id="Selected Year"
      margin="normal"
      select
      label="Selected Year"
      value={selectedYear}
  
      onChange={e => {
        props.startLoading();
        setSelectedYear(e.target.value);
      }}
     
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
     
        <div >
        <Fragment>
          
          <YearDropdown /> 
          <TableView
          
            title={title}
            backgroundImage={backgroundImage}
            table={props.annualReport.table}
          />
           
       </Fragment>
       </div>
      )}
      {props.annualReport.isLoading}
    </Fragment>
  );
}

const mapStateToProps = state => {
  return {
    annualReport: state.annualReport,
    loading: state.annualReport.loading,
    reload: state.annualReport.reload,
    isLoading: state.annualReport.isLoading
  };
};

export default connect(mapStateToProps, { getAnnualReport, startLoading })( withStyles(styles)(AnnualReport)
);