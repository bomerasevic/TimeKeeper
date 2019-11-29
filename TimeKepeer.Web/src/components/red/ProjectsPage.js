import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchProjects, projectSelect } from "../../store/actions/index";
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
const ProjectsPage = (props) => {
	const { classes } = props;
	const { data, loading, error } = props;
	const { fetchProjects, projectSelect } = props;
	let projects = data;
	useEffect(() => {
		fetchProjects();
		projects = data;
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
						<h2 className={classes.loaderText}>Please refresh application</h2>
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
										Projects
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
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "4%" }}>
											No.
								</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "10%" }}>Name</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize}>Customer</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize}>Team</CustomTableCell>

										<CustomTableCell className={classes.tableHeadFontsize} align="center" style={{ width: "5%" }} >
											Status
								</CustomTableCell>
										<CustomTableCell className={classes.tableHeadFontsize}  align="center"  >
											Actions
								</CustomTableCell>
									</TableRow>
								</TableHead>
								{props.user ? (
									<TableBody>
										{projects.map((p, i) => (
											<TableRow key={p.id}>
												<CustomTableCell>{p.id}</CustomTableCell>
												<CustomTableCell>{p.name}</CustomTableCell>
												<CustomTableCell>{p.customer.name}</CustomTableCell>
												<CustomTableCell>{p.team.name}</CustomTableCell>
												<CustomTableCell align="center">{p.status.name}</CustomTableCell>

												<CustomTableCell align="center">


													<Button aria-label="View" className=" deleteButton a-btn delete">
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
														<Button aria-label="Delete" className=" deleteButton a-btn delete"  style={{ color: "#9e1c13" }}>
															Delete
</Button>) : null}




												</CustomTableCell>
											</TableRow>
										))}
									</TableBody>



								) : null}

							</Table>
						</Paper>
					)
			}
		</React.Fragment >
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
		data: state.projects.data,
		loading: state.projects.loading,
		error: state.projects.error
	};
};
export default connect(mapStateToProps, { fetchProjects, projectSelect })(withStyles(styles)(ProjectsPage));