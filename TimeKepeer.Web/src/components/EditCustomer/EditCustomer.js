import React from "react";
import './EditCustomer.css';
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

class EditCustomer extends React.PureComponent {
    state = {};
    componentDidUpdate() {
        console.log("update");
        const { currentItem, selectedItemId } = this.props;
        console.log(currentItem);
        const loginForm = {
            initialValues: {
                name: currentItem.name ? currentItem.name : "",
                contact: currentItem.contact ? currentItem.contact : "",
                email: currentItem.email ? currentItem.email : "",
                phone: currentItem.phone ? currentItem.phone : "",

                status: currentItem.status ? currentItem.status.name : ""
            }
        };
    }

    render() {
        const { open, handleClickClose, currentItem, selectedItemId } = this.props;
        const loginForm = {
            initialValues: {
                name: currentItem.name ? currentItem.name : "",
                contact: currentItem.contact ? currentItem.contact : "",
                email: currentItem.email ? currentItem.email : "",
                phone: currentItem.phone ? currentItem.phone : "",

                status: currentItem.status ? currentItem.status.name : ""

            },
            validation: Yup.object({
                name: Yup.string().required("Field is Required"),
                contact: Yup.string().required("Field is Required"),
                email: Yup.string().required("Field is Required"),
                phone: Yup.string().required("Field is Required"),
                status: Yup.string().required("Field is Required")
            }),
            submit: (formValues, { setSubmitting }) => {
                let values = {};
                values = Object.assign(values, formValues);
                values.status = { id: 1 };
                values.address = { road: "3223 Commander Dr.", zipCode: "75001", city: "Dallas, USA", country: "Dallas, USA" }

                console.log(values);

                let putUrl = Config.apiUrl + "customers/1";
                console.log(Config);
                if (selectedItemId) {
                    values.id = selectedItemId;
                    axios
                        .put(`${Config.apiUrl}customers/${selectedItemId}`, values, {
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
                    console.log(values);
                    axios
                        .post(`${Config.apiUrl}customers`, values, {
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
                        {/* <DialogTitle id="form-dialog-title" className="text-center">
              {selectedItemId ? selectedItemId : "ID"}
            </DialogTitle> */}
                        <DialogContent>
                            <Formik
                                initialValues={loginForm.initialValues}
                                validationSchema={loginForm.validation}
                                onSubmit={loginForm.submit}
                            >
                                <Form>
                                    <Container>
                                        <Grid container spacing={4}>

                                            <Grid item xs={12} sm={8}>
                                                <Grid container spacing={4}>
                                                    <Grid item sm={12} md={12}>
                                                        <h4 className="m-0">
                                                            Customer Profile
                            </h4>
                                                    </Grid>
                                                    <Grid item sm={12} md={6}>
                                                        <div className="input-field">
                                                            <Field
                                                                name="name"
                                                                id="name"
                                                                type="text"
                                                                style={{ color: "black" }}
                                                            />
                                                            <label htmlFor="firstName">Bussines name</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="contact"
                                                                id="contact"
                                                                type="text"
                                                            />

                                                            <label htmlFor="contact">Contact name</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="email"
                                                                id="emailEmp"
                                                                type="email"
                                                                style={{ color: "black" }}
                                                            />
                                                            <label htmlFor="email">Email</label>
                                                        </div>
                                                        <div className="input-field">
                                                            <Field
                                                                name="phone"
                                                                id="phone"
                                                                type="text"
                                                            />

                                                            <label htmlFor="phone">Phone</label>
                                                        </div>

                                                    </Grid>

                                                    <Grid item xs={12} sm={6}>
                                                        <Field name="status" label="Status" />



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
                                                                    style={{ backgroundColor: "#26a69a", marginRight: "50px", width: "10px" }}
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
export default EditCustomer;