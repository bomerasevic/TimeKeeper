import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchProjects, projectSelect, projectDelete } from "../../store/actions/index";
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
import ProjectsModal from "./ProjectsModal";
const ProjectsPage = (props) => {
    const { classes, data, loading, error, selected, user, reload, fetchProjects, projectSelect, projectDelete } = props;

    let projects = data;
    useEffect(() => {
        fetchProjects();
        projects = data;
    }, [reload]);
    console.log("uSer u emply", user.user.user)
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
                            {selected ? <ProjectsModal selected={selected} open={true} /> : null}
                            <Toolbar className={classes.toolbar}>
                                <div>
                                    <Typography variant="h4" id="tableTitle" style={{ color: "white" }}>
                                        Projects
                            </Typography>
                                </div>
                                {user.user.user.role === "admin" ? (
                                    <div>
                                        <Tooltip title="Add">
                                            <Button
                                                aria-label="Add"
                                                className=" addButton btn add" style={{ color: "white", backgroundColor: "#26a69a" }}
                                                onClick={() => projectSelect(null, "add")}
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
                                        <CustomTableCell className={classes.tableHeadFontsize}> Name</CustomTableCell>
                                        <CustomTableCell className={classes.tableHeadFontsize}>Customer</CustomTableCell>
                                        <CustomTableCell className={classes.tableHeadFontsize} style={{ width: "6%" }} >Team</CustomTableCell>
                                       
								<CustomTableCell className={classes.tableHeadFontsize}  style={{ width: "6%" }} align="center">
                                            Status
                                </CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} 
										align="center" style={{ width: "14%" }}
										>
                                            Actions
                                </CustomTableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {projects.map((p, i) => (
                                        <TableRow key={p.id}>
                                            <CustomTableCell>{i + 1}</CustomTableCell>
                                            <CustomTableCell>{p.name}</CustomTableCell>
                                            <CustomTableCell>{p.customer.name}</CustomTableCell>
                                            <CustomTableCell>{p.team.name}</CustomTableCell>	
											<CustomTableCell>{p.status.name}</CustomTableCell>

                                            {user.user.user.role === "admin" ? (
                                                <CustomTableCell align="center">
                                                    <Button
                                                        aria-label="Edit"
                                                        //className={classes.editButton}
                                                        className=" editButton add a-btn"
                                                        style={{ color: "#1cba85" }}
                                                        onClick={() => projectSelect(p.id, "edit")}
                                                    >
                                                        Edit
                                            </Button>
                                                    <Button
                                                        aria-label="Delete"
                                                        //className={classes.deleteButton}
                                                        className=" button deleteButton a-btn delete"
                                                        style={{ color: "#9e1c13" }}
                                                        onClick={() => projectDelete(p.id)}
                                                    >
                                                        Delete
                                            </Button>
                                                </CustomTableCell>
                                            ) : (
                                                    <CustomTableCell align="center">
                                                        <IconButton aria-label="View" onClick={() => projectSelect(p.id, "view")}>
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
        data: state.projects.data,
        loading: state.projects.loading,
        error: state.projects.error,
        selected: state.projects.selected,
        user: state.user,
        reload: state.projects.reload
    };
};
export default connect(mapStateToProps, { fetchProjects, projectSelect, projectDelete })(
    withStyles(styles)(ProjectsPage)
);