import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchEmployees, employeeSelect } from "../../store/actions/index";
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
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import AddIcon from "@material-ui/icons/Add";
import VisibilityIcon from "@material-ui/icons/Visibility";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
const EmployeesPage = (props) => {
	const { classes } = props;
	const { data, loading, error } = props;
	const { fetchEmployees, employeeSelect } = props;
	let employees = data;
	useEffect(() => {
		fetchEmployees();
		employees = data;
	}, []);
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

							<Toolbar className={classes.toolbar}>
								<div>
									<Typography variant="h4" id="tableTitle" style={{ color: "white" }}>
										Employees
							</Typography>
								</div>
								<div>
								<Button aria-label="Add" className=" addButton btn add" style={{ color: "white" , backgroundColor:"#26a69a"}}>
															Add
                                             </Button>
								</div>
							</Toolbar>
							<Table className={classes.table}>
								<TableHead>
									<TableRow>
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "8%" }}>
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
								{props.user ? (<TableBody>
									{employees.map((e, i) => (
										<TableRow key={e.id}>
											<CustomTableCell>{e.id}</CustomTableCell>
											<CustomTableCell>{e.firstName}</CustomTableCell>
											<CustomTableCell>{e.lastName}</CustomTableCell>
											<CustomTableCell>{e.email}</CustomTableCell>
											<CustomTableCell>{e.phone}</CustomTableCell>
											<CustomTableCell align="center">


												<Button aria-label="View" className=" deleteButton a-btn delete">
													View
										</Button>
												{props.user.profile.role === "admin" ? (
													<Button
														aria-label="Edit"
														className=" editButton add a-btn"
														onClick={() => employeeSelect(e.id)}
														style={{ color: "#1cba85" }}
													>
														Edit
								</Button>) : null}
												{props.user.profile.role === "admin" ? (
													<Button aria-label="Delete" className=" deleteButton a-btn delete" style={{ color: "#9e1c13" }}>
														Delete
										</Button>) : null}



											</CustomTableCell>
										</TableRow>
									))}
								</TableBody>



								) : null}

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
		user: state.user.user,
		data: state.employees.data,
		loading: state.employees.loading,
		error: state.employees.error
	};
};
export default connect(mapStateToProps, { fetchEmployees, employeeSelect })(withStyles(styles)(EmployeesPage));