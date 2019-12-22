import React, { Fragment, useState, useEffect } from "react";

import TableView from "../../components/TableView/TableView";
import { MenuItem, TextField } from "@material-ui/core";
import { getAnnualReport, startLoading } from "../../store/actions/annualReportActions";
import { connect } from "react-redux";

import NavigationLogin from "../NavigationLogin/NavigationLogin";

function AnnualReport(props) {
  const [selectedYear, setSelectedYear] = useState(2019);
  const title = "Annual Overview";
  const backgroundImage = "/images/customers.jpg";

  useEffect(() => {
    props.getAnnualReport(selectedYear);
  }, [selectedYear]);

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

  return (
    <Fragment>
      < NavigationLogin />
      {!props.annualReport.isLoading && (
        <Fragment>
          <YearDropdown />
          <TableView
            title={title}
            backgroundImage={backgroundImage}
            table={props.annualReport.table}
          />
        </Fragment>
      )}
      {props.annualReport.isLoading}
    </Fragment>
  );
}

const mapStateToProps = state => {
  return {
    annualReport: state.annualReport
  };
};

export default connect(mapStateToProps, { getAnnualReport, startLoading })(AnnualReport);