import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchEmployees, employeeSelect, employeeDelete } from "../../store/actions/index";
import { withStyles } from "@material-ui/core/styles";
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
import styles from "./EmployeesStyles";
import VisibilityIcon from "@material-ui/icons/Visibility";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import EmployeesModal from "./EmployeesModal";
const EmployeesPage = (props) => {
    const { classes, data, loading, error, selected, user, reload,fetchEmployees, employeeSelect, employeeDelete } = props;
    
    let employees = data;
    useEffect(() => {
        fetchEmployees();
        employees = data;
    }, [reload]);
    return (
	
        <React.Fragment>
		<NavigationLogin />

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
                <Paper className={classes.root}>
                    {selected ? <EmployeesModal selected={selected} open={true} /> : null}
                    <Toolbar className={classes.toolbar}>
                        <div>
                            <Typography variant="h4" id="tableTitle" style={{ color: "white" }}>
                                Employees
                            </Typography>
                        </div>
                        {user.profile.role === "admin" ? (
                            <div>
                                <Tooltip title="Add">
                                    <Button
										aria-label="Add"
										className=" addButton btn add" style={{ color: "white", backgroundColor: "#26a69a" }}
                                        onClick={() => employeeSelect(null, "add")}
                                        //className={classes.hover}
                                    >
                                       Add
                                    </Button>
                                </Tooltip>
                            </div>
                        ) : null}
                    </Toolbar>
                    <Table className={classes.table}>
                        <TableHead>
                            <TableRow>
                                <CustomTableCell className={classes.tableHeadFontsize} style={{ width: "9%" }}>
                                    No.
                                </CustomTableCell>
                                <CustomTableCell className={classes.tableHeadFontsize}>First Name</CustomTableCell>
                                <CustomTableCell className={classes.tableHeadFontsize}>Last Name</CustomTableCell>
                                <CustomTableCell className={classes.tableHeadFontsize}>E-mail</CustomTableCell>
                                <CustomTableCell className={classes.tableHeadFontsize} style={{ width: "13%" }}>
                                    Phone
                                </CustomTableCell>
                                <CustomTableCell className={classes.tableHeadFontsize} align="center">
                                    Actions
                                </CustomTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {employees.map((e, i) => (
                                <TableRow key={e.id}>
                                    <CustomTableCell>{i + 1}</CustomTableCell>
                                    <CustomTableCell>{e.firstName}</CustomTableCell>
                                    <CustomTableCell>{e.lastName}</CustomTableCell>
                                    <CustomTableCell>{e.email}</CustomTableCell>
                                    <CustomTableCell>{e.phone}</CustomTableCell>
                                    {user.profile.role === "admin" ? (
                                        <CustomTableCell align="center">
                                            <Button
                                                aria-label="Edit"
												//className={classes.editButton}
												className=" editButton add a-btn"
																style={{ color: "#1cba85" }}
                                                onClick={() => employeeSelect(e.id, "edit")}
                                            >
                                                Edit
                                            </Button>
                                            <Button
                                                aria-label="Delete"
												//className={classes.deleteButton}
												className=" button deleteButton a-btn delete"
																style={{ color: "#9e1c13" }}
                                                onClick={() => employeeDelete(e.id)}
                                            >
                                                Delete
                                            </Button>
                                        </CustomTableCell>
                                    ) : (
                                        <CustomTableCell align="center">
                                            <IconButton aria-label="View" onClick={() => employeeSelect(e.id, "view")}>
                                                <VisibilityIcon />
                                            </IconButton>
                                        </CustomTableCell>
                                    )}
                                </TableRow>
                            ))}
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
    }
}))(TableCell);
const mapStateToProps = (state) => {
    return {
        data: state.employees.data,
        loading: state.employees.loading,
        error: state.employees.error,
        selected: state.employees.selected,
        user: state.user.user,
        reload: state.employees.reload
    };
};
export default connect(mapStateToProps, { fetchEmployees, employeeSelect, employeeDelete })(
    withStyles(styles)(EmployeesPage)
);