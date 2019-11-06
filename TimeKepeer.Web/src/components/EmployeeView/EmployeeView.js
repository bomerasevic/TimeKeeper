import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import "./EmployeeView.css";
import EmployeesList from "../EmployeesList/EmployeesList";
import Modal from "react-modal";
import * as Yup from "yup";

class EmployeeView extends React.Component {
    constructor() {
        super();
        this.state = {
            modalIsOpen: false
        };
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }
    openModal() {
        this.setState({ modalIsOpen: true });
    }
    closeModal() {
        this.setState({ modalIsOpen: false });
    }
    render() {
        const { classes } = this.props;
        return (
            <div>
                <NavigationLogin />
                <div className="row">
                    <h3 className="table-name">Employee</h3>
                    <a className="btn modal-trigger add-btn" onClick={this.openModal}>
                        Add employee
                    </a>
                </div>
                <Modal isOpen={this.state.modalIsOpen} onRequestClose={this.closeModal}>
                    <div className="row"></div>
                </Modal>

                <div class="table-employee">
                    <EmployeesList />
                </div>
            </div>
        );
    }
}
export default EmployeeView;
