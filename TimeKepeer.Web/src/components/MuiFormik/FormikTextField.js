import React, { PureComponent, Component, Fragment } from "react";
import { Formik, useField, Field, FastField } from "formik";
import { TextField } from "@material-ui/core";
export default function FormikTextField(props) {
    return (
        <FastField name={props.name}>
            {({
                field, // { name, value, onChange, onBlur }
                meta
            }) => {
                console.log("re-render textField");
                return (
                    <TextField
                        {...field}
                        margin="normal"
                        variant="outlined"
                        fullWidth={true}
                        label={props.label}
                        type={props.type}
                        InputLabelProps={props.type === "date" ? { shrink: true } : {}}
                        // floatingLabelFixed={props.type === "date" ? true : false}
                        error={Boolean(meta.touched && meta.error)}
                        helperText={meta.touched && meta.error ? meta.error : " "}
                    />
                );
            }}
        </FastField>
    );
}
