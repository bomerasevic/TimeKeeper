import React, { Fragment, useState, useEffect } from "react";
import TableView from "../TableView/TableView";
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
      {!props.monthlyReport.isLoading && (
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
      {props.monthlyReport.isLoading }
    </Fragment>
  );
}

const mapStateToProps = state => {
  return {
    monthlyReport: state.monthlyReport
  };
};

export default connect(mapStateToProps, { getMonthlyReport, startLoading })(MonthlyReport);