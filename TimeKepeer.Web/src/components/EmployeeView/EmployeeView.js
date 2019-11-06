import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import logo from "../../assets/images/puzzle.png";
import "./EmployeeView.css";
import EmployeesList from "../EmployeesList/EmployeesList";

function EmployeeView() {
    return (
        <div>
            <NavigationLogin />
            <div class="table-employee">
                <EmployeesList />
            </div>
        </div>
    );
}
export default EmployeeView;
