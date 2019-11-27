
import React from "react";
import './EditEmployee.css';
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import {
    Container,
    Grid,
    Paper,
    Dialog,
    DialogContent,
    Button,
    TextField
} from "@material-ui/core";
import TableTK from "../TableTK/TableTK";
import Config from "../../config";
import axios from "axios";
import Moment from "moment";
import image from "../../assets/images/pic02.jpg"

class EditEmployee extends React.PureComponent {
    state = {
        rolesTable: {
            head: ["Team", "Role"],
            rows: [
                {
                    id: 1,
                    team: { id: 0, name: "Bravo" },
                    role: { id: 0, name: "BE" }
                },
                {
                    id: 2,
                    team: { id: 1, name: "Delta" },
                    role: { id: 1, name: "FE" }
                },
            ]
        }
    };
    componentDidUpdate() {
        console.log("update");
        const { currentItem, selectedItemId } = this.props;
        console.log(currentItem);
        const loginForm = {
            initialValues: {
                firstName: currentItem.firstName ? currentItem.firstName : "",
                lastName: currentItem.lastName ? currentItem.lastName : "",
                email: currentItem.email ? currentItem.email : "",
                phone: currentItem.phone ? currentItem.phone : "",
                birthDate: currentItem.birthDate ? currentItem.birthDate : "",
                beginDate: currentItem.beginDate ? currentItem.beginDate : "",
                endDate: currentItem.endDate ? currentItem.endDate : "",
                status: currentItem.status ? currentItem.status.name : "",
                jobTitle: currentItem.position ? currentItem.position.name : ""
            }
        };
    }

    render() {
        const { open, handleClickClose, currentItem, selectedItemId } = this.props;
        const loginForm = {
            initialValues: {
                firstName: currentItem.firstName ? currentItem.firstName : "",
                lastName: currentItem.lastName ? currentItem.lastName : "",
                email: currentItem.email ? currentItem.email : "",
                phone: currentItem.phone ? currentItem.phone : "",
                birthDate: currentItem.birthDate ? currentItem.birthDate : "",
                beginDate: currentItem.beginDate ? currentItem.beginDate : "",
                endDate: currentItem.endDate ? currentItem.endDate : "",
                status: currentItem.status ? currentItem.status.name : "",
                jobTitle: currentItem.position ? currentItem.position.name : ""

            },
            validation: Yup.object({
                firstName: Yup.string().required("Field is Required"),
                lastName: Yup.string().required("Field is Required"),
                email: Yup.string().required("Field is Required"),
                phone: Yup.string().required("Field is Required"),
                birthDate: Yup.string().required("Field is Required"),
                beginDate: Yup.string().required("Field is Required"),
                endDate: Yup.string().required("Field is Required"),
                status: Yup.string().required("Field is Required"),
                jobTitle: Yup.string().required("Field is Required")

            }),
            submit: (formValues, { setSubmitting }) => {
                let values = {};
                values = Object.assign(values, formValues);
                values.image = "";
                values.status = { id: 1 };
                values.position = { id: 2 };
                delete values.jobTitle;
                let putUrl = Config.apiUrl + "employees/1";
                let postUrl = Config.apiUrl + "employees";
                console.log(Config);
                if (selectedItemId) {
                    values.id = selectedItemId;
                    axios
                        .put(`${Config.apiUrl}employees/${selectedItemId}`, values, {
                            headers: {
                                "Content-Type": "application/json",
                                Authorization: Config.token
                            }
                        })
                        .then(res => {
                            console.log(res);
                            handleClickClose();
                        })
                        .catch(err => {
                            console.log("error", err);
                            alert("Something went wrong");
                        })
                        .finally(() => setSubmitting(false));
                } else {
                    delete values.endDate;
                    console.log(values);
                    axios
                        .post(`${Config.apiUrl}employees`, values, {
                            headers: {
                                "Content-Type": "application/json",
                                Authorization: Config.token
                            }
                        })
                        .then(res => {
                            console.log(res);
                            handleClickClose();
                        })
                        .catch(err => {
                            console.log("error", err);
                            alert("Something went wrong");
                        })
                        .finally(() => setSubmitting(false));
                }

            }
        };
        return (
            <React.Fragment>
                <Dialog
                    open={open}
                    onClose={handleClickClose}
                    aria-labelledby="form-dialog-title"
                    maxWidth="lg"
                >
                    <div className="m-1">
                        {}
                        <DialogContent>
                            <Formik
                                initialValues={loginForm.initialValues}
                                validationSchema={loginForm.validation}
                                onSubmit={loginForm.submit}
                            >
                                <Form>
                                    <Container>
                                        <Grid container spacing={4}>
                                            <Grid item xs={12} sm={4} style={{ textAlign: "center" }}>



                                                {!selectedItemId && (
                                                    <img
                                                        src=""
                                                        alt=""
                                                        className="profile-picture-fit mb-1-5"
                                                        style={{ width: "300px", height: "auto" }}
                                                    />
                                                )}

                                                {selectedItemId && (
                                                    <div><img
                                                        src={image}
                                                        alt=""
                                                        className="profile-picture-fit mb-1-5"
                                                        style={{ width: "300px", height: "auto" }}
                                                    />
                                                        <Button
                                                            className="mb-1-5"
                                                            type="button"
                                                            variant="contained"
                                                            color="default"
                                                            className=" btn modal-trigger add-btn"
                                                            fullWidth={true}
                                                            style={{ backgroundColor: "#26a69a", textAlign: "center", marginBottom: "10px" }}
                                                        >
                                                            Upload Photo
                        </Button>
                                                        <TableTK
                                                            head={this.state.rolesTable.head}
                                                            rows={this.state.rolesTable.rows.map(row => {
                                                                return {
                                                                    team: row.team.name,
                                                                    role: row.role.name
                                                                };
                                                            })}
                                                        />
                                                    </div>
                                                )}
                                                {!selectedItemId && (
                                                    <div></div>
                                                )}

                                            </Grid>
                                            <Grid item xs={12} sm={8}>
                                                <Grid container spacing={4}>
                                                    <Grid item sm={12} md={12}>
                                                        <h4 className="m-0">
                                                            Employee Profile
                            </h4>
                                                    </Grid>
                                                    <Grid item sm={12} md={6}>
                                                        <div className="input-field">
                                                            <Field
                                                                name="firstName"
                                                                id="firstName"
                                                                type="text"
                                                            />
                                                            <label htmlFor="firstName">First name</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="lastName"
                                                                id="lastname"
                                                                type="text"
                                                            />

                                                            <label htmlFor="lastName">Last name</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="email"
                                                                id="emailEmp"
                                                                type="email"
                                                                style={{ color: "black !important" }}
                                                            />
                                                            <label htmlFor="email">Email</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="phone"
                                                                id="phone"
                                                                type="text"
                                                                style={{ color: "black !important" }}
                                                            />

                                                            <label htmlFor="phone">Phone</label>
                                                        </div>
                                                        <div className="input-field birthday">
                                                            <Field
                                                                name="birthDate"
                                                                id="birthDate"
                                                                type="date"
                                                            />

                                                            <label htmlFor="birthDate">Birthday</label>
                                                        </div>
                                                    </Grid>
                                                    <Grid item xs={12} sm={6}>
                                                        <div className="input-field datetime">
                                                            <Field
                                                                name="beginDate"
                                                                id="beginDate"
                                                                type="date"
                                                            />
                                                            <label htmlFor="beginDate">Employee begin date</label>
                                                        </div>
                                                        <div className="input-field datetime">
                                                            <Field
                                                                name="endDate"
                                                                id="endDate"
                                                                type="date"
                                                            />
                                                            <label htmlFor="endDate">Employee end date</label>
                                                        </div>
                                                        <Field name="status" label="Status" />
                                                        <Field
                                                            name="jobTitle"
                                                            label="Job Title"
                                                        />


                                                        <div className="text-right">
                                                            {selectedItemId && (
                                                                <Button
                                                                    type="submit"
                                                                    variant="contained"
                                                                    color="primary"
                                                                    className="mr-1"
                                                                    style={{ backgroundColor: "#26a69a" }}
                                                                >
                                                                    Update
                                </Button>
                                                            )}
                                                            {!selectedItemId && (
                                                                <Button
                                                                    type="submit"
                                                                    variant="contained"
                                                                    className="mr-1 bg-success text-light"
                                                                    style={{ backgroundColor: "#26a69a", marginRight: "50px", width: "100px" }}
                                                                >
                                                                    Add
                                </Button>
                                                            )}
                                                            <Button
                                                                type="button"
                                                                variant="contained"
                                                                color="default"
                                                                onClick={handleClickClose}
                                                            >
                                                                Cancel
                              </Button>
                                                        </div>
                                                    </Grid>
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                    </Container>
                                </Form>
                            </Formik>
                        </DialogContent>
                    </div>
                </Dialog>
            </React.Fragment >
        );
    }
}
export default EditEmployee;