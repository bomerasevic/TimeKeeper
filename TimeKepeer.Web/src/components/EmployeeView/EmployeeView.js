import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import logo from "../../assets/images/puzzle.png";
import "./EmployeeView.css";
import EmployeesList from "../EmployeesList/EmployeesList";

function EmployeeView() {
    return (
        <div>
            <NavigationLogin />
            <div className="row">
                <h3 className="table-name">Employees</h3>
                <a className=" btn modal-trigger add-btn">Add employee</a>
            </div>
            <div class="table-employee">
                <EmployeesList />
            </div>
        </div>
    );
}
export default EmployeeView;
