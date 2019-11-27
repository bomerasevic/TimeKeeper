import React, { Component, Fragment } from "react";
import TableView from "../TableView/TableView";
import EditEmployee from "../EditEmployee/EditEmployee";
import axios from "axios";
import config from "../../config";
import Moment from "moment";
import Navigation from "../NavigationLogin/NavigationLogin";

class EmployeesView extends Component {
	state = {
		title: "Employees",
		table: {
			head: [],
			rows: [],
			actions: true
		},
		isEditOpen: false,
		selectedItemId: null,
		currentItem: {}
	};
	handleClickOpen = (itemId) => {
		if (itemId !== null) {
			axios
				.get(config.apiUrl + "employees/" + itemId, {
					headers: {
						"Content-Type": "application/json",
						Authorization: config.token
					}
				})
				.then((res) => {
					console.log("unformatted", res.data.birthDate);
					let formattedData = res.data;
					formattedData.birthDate = Moment(formattedData.birthDate).format("YYYY-MM-DD");
					formattedData.beginDate = Moment(formattedData.beginDate).format("YYYY-MM-DD");
					formattedData.endDate = Moment(formattedData.endDate).format("YYYY-MM-DD");
					console.log("formatted", formattedData.birthDate);
					this.setState({
						isEditOpen: true,
						selectedItemId: itemId,
						currentItem: formattedData
					});
				})
				.catch((err) => {
					alert(err);
					console.log(err);
				});
		} else {
			this.setState({
				isEditOpen: true,
				selectedItemId: null,
				currentItem: {}
			});
		}
		// console.log(itemId);
	};
	handleClickClose = () => {
		this.setState({ isEditOpen: false, selectedItemId: null, currentItem: {} });
	};
	componentDidMount() {
		axios(`${config.apiUrl}employees`, {
			headers: {
				"Content-Type": "application/json",
				Authorization: config.token
			}
		})
			.then((res) => {
				// console.log(res);
				let data = res.data.map((row) => {
					return {
						id: row.id,
						firstName: row.firstName,
						lastName: row.lastName,
						email: row.email,
						phone: row.phone
					};
				});
				// console.log('mapped', data);
				this.setState({
					table: {
						rows: data,
						head: Object.keys(data[0]),
						actions: true
					}
				});
			})
			.catch((err) => {
				alert(err);
				console.log(err);
			});
	}
	render() {
		return (
			<Fragment>
				<Navigation />
				<TableView {...this.state} handleClickOpen={this.handleClickOpen} />
				<EditEmployee
					open={this.state.isEditOpen}
					handleClickClose={this.handleClickClose}
					selectedItemId={this.state.selectedItemId}
					currentItem={this.state.currentItem}
				/>
			</Fragment>
		);
	}
}
export default EmployeesView;