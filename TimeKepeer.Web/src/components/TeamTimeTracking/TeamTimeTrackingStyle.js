import { ActionImportantDevices } from "material-ui/svg-icons";

const styles = (theme) => ({
	root: {
		width: "90%",
		margin: "auto",
		marginTop: theme.spacing(3),
		overflowX: "auto"
    },
    headerTable : {
     backgroundColor: "rgb(54,52,63)"
    },
	toolbar: {
		display: "flexbox",
		justifyContent: "space-between",
		padding: "1.2rem",
		backgroundColor: "rgb(54,52,64)",
		opacity: "0.95"
	},
	table: {
		minWidth: 1000
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
        fontSize: "1.1rem",
		backgroundColor: "rgb(54,52,64)",
		opacity: "0.95"
        
	},
	loader: {
		color: "#26a69a"
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
    
    dropDown: {
		display: "flex",
		flexDirection: "row-reverse",
		color: "white"


	},
	selectors: {
color: "white"
	},
	editButton: {
		fill: "green",
		"&:hover": {
			backgroundColor: "rgba(0,153,0,.1) !important"
		}
	}
});
export default styles;