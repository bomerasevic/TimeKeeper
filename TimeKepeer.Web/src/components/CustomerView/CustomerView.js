import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import logo from "../../assets/images/puzzle.png";
import "./CustomerView.css";
import CustomerList from "../CustomerList/CustomerList";

function CustomerView() {
    return (
        <div>
            <NavigationLogin />
            <div className="row">
                <h3 className="table-name">Customers</h3>
                <a className=" btn modal-trigger add-btn">Add customer</a>
            </div>
            <div class="table-customers">
                <CustomerList />
            </div>
        </div>
    );
}
export default CustomerView;