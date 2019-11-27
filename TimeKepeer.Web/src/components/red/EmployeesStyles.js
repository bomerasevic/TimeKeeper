const styles = (theme) => ({
	root: {
		width: "90%",
		margin: "auto",
		marginTop: theme.spacing(3),
		overflowX: "auto"
	},
	toolbar: {
		display: "flexbox",
		justifyContent: "space-between",
		padding: "1.2rem",
		backgroundColor: "#40454F"
	},
	table: {
		minWidth: 700
	},
	button: {
		color: "white",
		fontSize: "1.2rem",
		padding: "5px",
		margin: "3px",
		color: "#A3A6B4"
	},
	tableHeadFontsize: {
		textTransform: "uppercase",
		fontWeight: "500",
		fontSize: "1.1rem"
	},
	loader: {
		color: "blue"
	},
	loaderText: {
		color: "white",
		marginTop: "2rem"
	},
	center: {
		display: "flex",
		flexDirection: "column",
		justifyContent: "center",
		alignItems: "center"
	},
	hover: {
		"&:hover": {
			backgroundColor: "#707580 !important"
		}
	},
	deleteButton: {
		"&:hover": {
			backgroundColor: "red !important",
			margin: "20px !important"
		}
	},
	editButton: {
		fill: "green",
		"&:hover": {
			backgroundColor: "rgba(0,153,0,.1) !important"
		}
	}
});
export default styles;