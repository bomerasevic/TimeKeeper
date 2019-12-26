import React, { useEffect } from "react";
import { connect } from "react-redux";
import { withStyles } from "@material-ui/core/styles";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Paper,
  Button,
  CircularProgress,
  Backdrop,
  Toolbar,
  Typography
} from "@material-ui/core";
import styles from "./TeamTimeTrackingStyle";
import DropDownMonth from "../TeamTimeTracking/DropDownMonth";
import DropDownYear from "../TeamTimeTracking/DropDownYear";
import DropDownTeam from "../TeamTimeTracking/DropDownTeam";
import { fetchTeamTracking } from "../../store/actions/index";
import Navigation from "../NavigationLogin/NavigationLogin";
const TeamTimeTracking = (props) => {
  const { classes } = props;
  const { data, loading, error, teams, year, month, fetchTeamTracking } = props;

  useEffect(() => {
    fetchTeamTracking(teams.selectedTeam, year, month);
  }, [teams.selectedTeam, year, month, fetchTeamTracking]);

  return (

    <React.Fragment>
      <Navigation />
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
            <h2 className={classes.loaderText}>
              Please reload the application
            </h2>
            <Button
              variant="outlined"
              size="large"
              className={classes.loaderText}
            >
              Reload
            </Button>
          </div>
        </Backdrop>
      ) : (
            <Paper className={classes.root}>
              <Toolbar className={classes.toolbar}>
                <div>
                  <Typography
                    variant="h4"
                    id="tableTitle"
                    style={{ color: "white" }}
                  >
                    Team Time Tracking
              </Typography>
               <div className={classes.dropDown}>
                  <DropDownYear className={classes.dropDown}></DropDownYear>
                  <DropDownMonth className={classes.dropDown}></DropDownMonth>
                  <DropDownTeam className={classes.dropDown}></DropDownTeam>
                  </div>
                  {/* <Button
                    onClick={() => {
                      console.log("PROPS", props);
                      fetchTeamTracking(teams.selectedTeam, year, month);
                    }}
                  >
                    FETCH THEM
              </Button> */}
                </div>
              </Toolbar>
              <Table className={classes.table}>
                <TableHead
                className= {classes.headerTabel}>
                  <TableRow>
                    <CustomTableCell
                      className={classes.tableHeadFontsize}
                      style={{ width: "14.28%" }}
                    >
                      Employee
                </CustomTableCell>
                    <CustomTableCell className={classes.tableHeadFontsize}
                     style={{ width: "14.28%" }}>
                      Working hours
                </CustomTableCell>
                    <CustomTableCell className={classes.tableHeadFontsize}
                     style={{ width: "14.28%" }}>
                      Business absence
                </CustomTableCell>
                    <CustomTableCell className={classes.tableHeadFontsize}
                     style={{ width: "14.28%" }}>
                      Public holiday
                </CustomTableCell>
                    <CustomTableCell
                      className={classes.tableHeadFontsize}
                      style={{ width: "14.28%" }}
                    >
                      Vacation
                </CustomTableCell>
                    <CustomTableCell className={classes.tableHeadFontsize}
                     style={{ width: "14.28%" }}>
                      Sick days
                </CustomTableCell>
                    <CustomTableCell className={classes.tableHeadFontsize}
                     style={{ width: "14.28%" }}>
                      Missing entries
                </CustomTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data.data !== undefined ?
                    data.data.map((r, i) => (
                      <TableRow key={r.id}>
                        <CustomTableCell>{r.employee.name}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.workday}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.business}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.holiday}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.vacation}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.sick}</CustomTableCell>
                        <CustomTableCell>{r.hourTypes.other}</CustomTableCell>
                      </TableRow>
                    )) : null}
                </TableBody>
              </Table>
            </Paper>
          )}
    </React.Fragment>
  );
};

const CustomTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: "#40454F",
    color: "white",
    width: "20%"
  },
  body: {
    fontSize: 14
  },

}))(TableCell);

const mapStateToProps = (state) => {
  return {
    user: state.user.user,
    data: state.teamTracking,
    teams: state.teams,
    year: state.year.selected,
    month: state.month.selectedMonth
  };
};

export default connect(mapStateToProps, {
  fetchTeamTracking
})(withStyles(styles)(TeamTimeTracking));