import React, { Component, Fragment } from "react";
import TableView from "../TableView/TableView";
import EditCustomer from "../EditCustomer/EditCustomer";
import axios from "axios";
import config from "../../config";
import Moment from "moment";
import Navigation from "../NavigationLogin/NavigationLogin"
import EditProject from "../EditProject/EditProject";
class CustomersView extends Component {
    state = {
        title: "Projects",
        table: {
            head: [],
            rows: [],
            actions: true
        },
        isEditOpen: false,
        selectedItemId: null,
        currentItem: {}
    };
    handleClickOpen = itemId => {
        if (itemId !== null) {
            axios
                .get(config.apiUrl + "projects/" + itemId, {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: config.token
                    }
                })
                .then(res => {
                    let formattedData = res.data;

                    this.setState({
                        isEditOpen: true,
                        selectedItemId: itemId,
                        currentItem: formattedData
                    });
                })
                .catch(err => {
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

        axios(`${config.apiUrl}projects`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        })
            .then(res => {
                console.log(res);
                let data = res.data.map(row => {
                    return {
                        id: row.id,
                        name: row.name,
                        customer: row.customer.name,
                        team: row.team.name,
                        status: row.status.name
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
            .catch(err => {
                alert(err);
                console.log(err);
            });
    }
    render() {
        return (
            <Fragment>
                <Navigation />
                <TableView {...this.state} handleClickOpen={this.handleClickOpen} />
                <EditProject
                    open={this.state.isEditOpen}
                    handleClickClose={this.handleClickClose}
                    selectedItemId={this.state.selectedItemId}
                    currentItem={this.state.currentItem}
                />
            </Fragment>
        );
    }
}
export default CustomersView;