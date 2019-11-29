import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchCustomers, customerSelect } from "../../store/actions/index";
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
const CustomersPage = (props) => {
	const { classes } = props;
	const { data, loading, error } = props;
	const { fetchCustomers, customerSelect } = props;
	let customers = data;
	useEffect(() => {
		fetchCustomers();
		customers = data;
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
										Customers:
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
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "1%" }}>
											No.
								</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} >Name</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize}>Contact</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize}>E-mail</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "10%" }}>
											Phone
								</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "1%" }}>
											Status
								</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} align="center">
											Actions
								</CustomTableCell>
									</TableRow>
								</TableHead>
								{props.user ? (
									<TableBody>
										{customers.map((c, i) => (
											<TableRow key={c.id}>
												<CustomTableCell>{c.id}</CustomTableCell>
												<CustomTableCell>{c.name}</CustomTableCell>
												<CustomTableCell>{c.contact}</CustomTableCell>
												<CustomTableCell>{c.email}</CustomTableCell>
												<CustomTableCell>{c.phone}</CustomTableCell>
												<CustomTableCell>{c.status.name}</CustomTableCell>
												<CustomTableCell align="center">

													<Button aria-label="View" className=" button deleteButton a-btn delete">
														View
</Button>
													{props.user.profile.role === "admin" ? (
														<Button
															aria-label="Edit"
															className=" editButton add a-btn"
															style={{ color: "#1cba85" }}

														>
															Edit
</Button>) : null}
													{props.user.profile.role === "admin" ? (
														<Button aria-label="Delete" className=" button deleteButton a-btn delete"
														style={{ color: "#9e1c13" }}>
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
		data: state.customers.data,
		loading: state.customers.loading,
		error: state.customers.error
	};
};
export default connect(mapStateToProps, { fetchCustomers, customerSelect })(withStyles(styles)(CustomersPage));